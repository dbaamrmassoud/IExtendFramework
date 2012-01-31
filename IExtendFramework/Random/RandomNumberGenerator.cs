using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace IExtendFramework.Random
{
	/// <summary>
	/// A Random Number Generator
	/// </summary>
	/// <remarks></remarks>
	public class RandomNumberGenerator : System.Random
	{
	    System.Random RNG = new System.Random(DateTime.Now.Millisecond);
	    private System.ComponentModel.BackgroundWorker _internalWorker = new System.ComponentModel.BackgroundWorker();
	    private System.ComponentModel.BackgroundWorker Worker {
	        get { return _internalWorker; }
	        set {
	            if (_internalWorker != null) {
	                _internalWorker.DoWork -= Worker_DoWork;
	                _internalWorker.ProgressChanged -= Worker_ReportProgress;
	            }
	            _internalWorker = value;
	            if (_internalWorker != null) {
	                _internalWorker.DoWork += Worker_DoWork;
	                _internalWorker.ProgressChanged += Worker_ReportProgress;
	            }
	        }
	    }
	    /// <summary>
	    /// This Generates the Number
	    /// </summary>
	    /// <param name="Minimum">The Minimum number</param>
	    /// <param name="Maximum">The Maximum number</param>
	    /// <returns>The randomized number</returns>
	    /// <remarks></remarks>
	    public int Generate(int Minimum, int Maximum)
	    {
	        return RNG.Next(Minimum, Maximum);
	    }
	
	    public RandomNumberGenerator()
	    {
	        Worker.RunWorkerAsync();
	    }
	
	    public RandomNumberGenerator(int _Seed)
	    {
	        Worker.RunWorkerAsync();
	        RNG = new System.Random(_Seed);
	    }
	
	    private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
	    {
	        Worker.WorkerReportsProgress = true;
	        while (true) {
	            Worker.ReportProgress(0);
	            System.Threading.Thread.Sleep(100);
	        }
	    }
	
	    private void Worker_ReportProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
	    {
	        RNG = new System.Random(RNG.Next());
	    }
	    
	    public override int Next()
	    {
	        return RNG.Next();
	    }
	    
	    public override int Next(int minValue, int maxValue)
	    {
	        return Generate(minValue, maxValue);
	    }
	    
	    public override int Next(int maxValue)
	    {
	        return RNG.Next(maxValue);
	    }
	    
	    public override string ToString()
	    {
	        return "[IExtendFramework.RandomNumberGenerator: Inherits System.Random]";
	    }
	    
	    public override double NextDouble()
	    {
	        return RNG.NextDouble();
	    }
	
	}
}
