using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace IExtendFramework.Events
{
    /// <summary>
    /// This delegate describes the signature of the event, which is emitted by the generated helper classes.
    /// </summary>
    public delegate void CommonEventHandlerDelegate(Type EventType, object[] args);

    /// <summary>
    /// Creates EventHandlers for every type of event
    /// </summary>
    public class CommonEventHandler
    {
        private static Hashtable eventHandlers = new Hashtable();
        private EventHandlerTypeEmitter emitter;
        private string helperName;

        /// <summary>
        /// Creates the CommonEventHandler.
        /// </summary>
        /// <param name="Name">Name of the Factory. Is used as naming component of the event handlers to create.</param>
        public CommonEventHandler(string Name)
        {
            helperName = Name;
            emitter = new EventHandlerTypeEmitter(Name);
        }

        /// <summary>
        /// Creates an event handler for the specified event
        /// </summary>
        /// <param name="Info">The event info class of the event</param>
        /// <returns>The created event handler help class.</returns>
        public object GetEventHandler(EventInfo Info)
        {
            string handlerName = helperName + Info.Name;
            object eventHandler = eventHandlers[handlerName];
            if (eventHandler == null)
            {
                Type eventHandlerType = emitter.GetEventHandlerType(Info);

                // Call constructor of event handler type to create event handler
                ConstructorInfo myCtor = eventHandlerType.GetConstructor(new Type[] { typeof(EventInfo) });
                object[] ctorArgs = new object[] { Info };
                eventHandler = myCtor.Invoke(ctorArgs);

                eventHandlers.Add(handlerName, eventHandler);
            }
            return eventHandler;
        }

        private class EventHandlerTypeEmitter
        {
            private static Hashtable handlerTypes = new Hashtable();
            string assemblyName;
            AssemblyBuilder asmBuilder = null;
            ModuleBuilder helperModule = null;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="Name">The name, which the is given to assembly, module and class.</param>
            public EventHandlerTypeEmitter(string Name)
            {
                assemblyName = Name;
            }

            /// <summary>
            /// Emits dynamically a event handler with a given signature, which fills all arguments of the event in an object array
            /// and calls a common event.
            /// </summary>
            /// <returns></returns>
            public Type GetEventHandlerType(EventInfo Info)
            {
                string handlerName = assemblyName + Info.Name;
                Type tpEventHandler = (Type)handlerTypes[handlerName];
                if (tpEventHandler == null)
                {
                    // Create the type
                    tpEventHandler = EmitHelperClass(handlerName, Info);
                    handlerTypes.Add(handlerName, tpEventHandler);
                }
                return tpEventHandler;
            }

            private Type EmitHelperClass(string HandlerName, EventInfo Info)
            {
                if (helperModule == null)
                {
                    AssemblyName myAsmName = new AssemblyName();
                    myAsmName.Name = assemblyName + "Helper";
                    asmBuilder = Thread.GetDomain().DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run);
                    helperModule = asmBuilder.DefineDynamicModule(assemblyName + "Module", true);
                }

                //////////////////////////////////////////////////////////////////////////
                // Define Type
                //////////////////////////////////////////////////////////////////////////
                TypeBuilder helperTypeBld = helperModule.DefineType(HandlerName + "Helper", TypeAttributes.Public);

                // Define fields
                FieldBuilder typeField = helperTypeBld.DefineField("eventType", typeof(Type), FieldAttributes.Private);
                FieldBuilder eventField = helperTypeBld.DefineField("CommonEvent", typeof(CommonEventHandlerDelegate), FieldAttributes.Private);
                EventBuilder commonEvent = helperTypeBld.DefineEvent("CommonEvent", EventAttributes.None, typeof(CommonEventHandlerDelegate));

                //////////////////////////////////////////////////////////////////////////
                // Build Constructor
                //////////////////////////////////////////////////////////////////////////
                Type objType = Type.GetType("System.Object");
                ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);

                // Build constructor with arguments (Type)
                Type[] ctorParams = new Type[] { typeof(EventInfo) };
                ConstructorBuilder ctor = helperTypeBld.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctorParams);

                // Call constructor of base class
                ILGenerator ctorIL = ctor.GetILGenerator();
                ctorIL.Emit(OpCodes.Ldarg_0);
                ctorIL.Emit(OpCodes.Call, objCtor);

                // store first argument to typeField
                ctorIL.Emit(OpCodes.Ldarg_0);
                ctorIL.Emit(OpCodes.Ldarg_1);
                ctorIL.Emit(OpCodes.Stfld, typeField);

                // return
                ctorIL.Emit(OpCodes.Ret);
                // Now constructor is finished!

                //////////////////////////////////////////////////////////////////////////
                // Build customized event handler
                //////////////////////////////////////////////////////////////////////////
                string eventName = Info.Name;
                Type ehType = Info.EventHandlerType;
                MethodInfo mi = ehType.GetMethod("Invoke");
                ParameterInfo[] eventParams = mi.GetParameters();

                // Build the type list of the parameter
                int paramCount = eventParams.Length;
                Type[] invokeParams = new Type[paramCount];
                for (int i = 0; i < paramCount; i++)
                {
                    invokeParams[i] = eventParams[i].ParameterType;
                }

                string methodName = "CustomEventHandler";
                MethodAttributes attr = MethodAttributes.Public | MethodAttributes.HideBySig;
                MethodBuilder invokeMthd = helperTypeBld.DefineMethod(methodName, attr, typeof(void), invokeParams);

                // A ILGenerator can now be spawned, attached to the MethodBuilder.
                ILGenerator ilGen = invokeMthd.GetILGenerator();

                // Create Label before return
                Label endOfMethod = ilGen.DefineLabel();

                // Create a local variable of type �string�, and call it �args�
                LocalBuilder localObj = ilGen.DeclareLocal(typeof(object[]));
                localObj.SetLocalSymInfo("args"); // Provide name for the debugger.

                // create object array of proper length
                ilGen.Emit(OpCodes.Ldc_I4, paramCount);
                ilGen.Emit(OpCodes.Newarr, typeof(object));
                ilGen.Emit(OpCodes.Stloc_0);

                // Now put all arguments in the object array
                for (int i = 0; i < paramCount; i++)
                {
                    byte i1b = Convert.ToByte(i + 1);
                    ilGen.Emit(OpCodes.Ldloc_0);
                    ilGen.Emit(OpCodes.Ldc_I4, i);
                    ilGen.Emit(OpCodes.Ldarg_S, i1b);

                    // Is argument value type?
                    if (invokeParams[i].IsValueType)
                    {
                        // Box the value type
                        ilGen.Emit(OpCodes.Box, invokeParams[i]);
                    }
                    // Put the argument in the object array
                    ilGen.Emit(OpCodes.Stelem_Ref);
                }

                // raise common event
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldfld, eventField);
                ilGen.Emit(OpCodes.Brfalse_S, endOfMethod);
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldfld, eventField);
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldfld, typeField);
                ilGen.Emit(OpCodes.Ldloc_0);
                MethodInfo raiseEventMethod = typeof(CommonEventHandlerDelegate).GetMethod("Invoke", BindingFlags.Public | BindingFlags.Instance);
                if (raiseEventMethod == null) throw new ApplicationException("CommonEventHandlerDlg:Invoke not found");
                ilGen.Emit(OpCodes.Callvirt, raiseEventMethod);

                // return
                ilGen.MarkLabel(endOfMethod);
                ilGen.Emit(OpCodes.Ret);

                //////////////////////////////////////////////////////////////////////////
                // add_CommonEvent
                //////////////////////////////////////////////////////////////////////////
                methodName = "add_CommonEvent";
                attr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
                invokeParams = new Type[1];
                invokeParams[0] = typeof(CommonEventHandlerDelegate);
                MethodBuilder addMethod = helperTypeBld.DefineMethod(methodName, attr, typeof(void), invokeParams);
                ilGen = addMethod.GetILGenerator();

                // define code
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldfld, eventField);
                ilGen.Emit(OpCodes.Ldarg_1);

                Type[] combineTypes = new Type[] { typeof(Delegate), typeof(Delegate) };
                MethodInfo combineMtd = typeof(Delegate).GetMethod("Combine", combineTypes);
                if (combineMtd == null) throw new ApplicationException("Delegate:Combine not found");
                ilGen.Emit(OpCodes.Call, combineMtd);
                ilGen.Emit(OpCodes.Castclass, typeof(CommonEventHandlerDelegate));
                ilGen.Emit(OpCodes.Stfld, eventField);
                ilGen.Emit(OpCodes.Ret);

                // Set add method
                commonEvent.SetAddOnMethod(addMethod);

                //////////////////////////////////////////////////////////////////////////
                // remove_CommonEvent
                //////////////////////////////////////////////////////////////////////////
                methodName = "remove_CommonEvent";
                attr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
                invokeParams = new Type[1];
                invokeParams[0] = typeof(CommonEventHandlerDelegate);
                MethodBuilder removeMethod = helperTypeBld.DefineMethod(methodName, attr, typeof(void), invokeParams);
                ilGen = removeMethod.GetILGenerator();

                // define code
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldfld, eventField);
                ilGen.Emit(OpCodes.Ldarg_1);

                MethodInfo removeMtd = typeof(Delegate).GetMethod("Remove", combineTypes);
                if (removeMtd == null) throw new ApplicationException("Delegate:Remove not found");
                ilGen.Emit(OpCodes.Call, removeMtd);
                ilGen.Emit(OpCodes.Castclass, typeof(CommonEventHandlerDelegate));
                ilGen.Emit(OpCodes.Stfld, eventField);
                ilGen.Emit(OpCodes.Ret);

                // Set remove method
                commonEvent.SetRemoveOnMethod(removeMethod);

                //////////////////////////////////////////////////////////////////////////
                // Now event handler is finished!
                //////////////////////////////////////////////////////////////////////////
                return helperTypeBld.CreateType();
            }
        }
    }
}
