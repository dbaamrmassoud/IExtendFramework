   M S B A M L   `   `   `  ����R  NIExtendFramework, Version=1.0.4679.22611, Culture=neutral, PublicKeyToken=nullk:clr-namespace:IExtendFramework.Controls.AdvancedMessageBox,IExtendFramework.Controls.AdvancedMessageBox  5      >    8IExtendFramework.Controls.AdvancedMessageBox.CommandLink   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Llocal:clr-namespace:IExtendFramework.Controls.AdvancedMessageBox     5      -   5          >�Name$  UserControl��5	       J� SnapsToDevicePixels . &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5       � 	Resources 5       �� MergedDictionaries �� SourceK  EIExtendFramework.Controls.AdvancedMessageBox.CommandLinkTextConverter   CommandLinkTextConverterZ  TIExtendFramework.Controls.AdvancedMessageBox.CommandLinkExtraTextVisibilityConverter + 'CommandLinkExtraTextVisibilityConverter�� 5       5      �� 5      $&  ResourceDictionaries/Common.xaml?�6   %:   &	        &	        5      6$     5      63   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      �� -   5         ��Name$ CommandLinkButton��5        NoChromeButton#���  6&      CommandButtonControlTemplate#���  6O   ��6�   �� *	Command+ �� Mode$ OneWay=���6�   �� *ID+$ OneWay=� 5      	 �� 
TargetType
 ��Name �� CornerRadius �� Padding �� EndPoint �� 
StartPoint � Color � Offset'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition   From   GeneratedDuration	   To   States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value9	  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value �� 
TargetType �� Property  �� Value! � ColumnDefinitions" � RowDefinitions# ��Name  CmdArr1$ J� Effect3
  -System.Windows.Media.Effects.DropShadowEffect% 
  	Direction& 
  
BlurRadius' 
  ShadowDepth( 
  Opacity  CmdArr2) J� Opacity* ��Name+ �� ConverterParameter, �� 	Converter- �� Triggers. P� Property/ P� Value0 �� 
TargetName��%�  &	       7   7   7  �� 5      "	 ��6;   .R�5      �� $
 CommandLinkBorder��6   $��1q�6)   $ 2��6=   $ 9q�6N   
������ ��5       �� 5!   	   $ 0.5,1&�6   $ 0.5,0&�6.   .��5"   
   � $ White��6   $ 0Q�6%   � 5#   
   $ White��6   $
 0.33Q�6%   � 5$   
   $ White��6   $ 1Q�6%   5%   
    5'        5(   	   CommonStates�� 5)   
     5*      $ 	MouseOver��6   $ 0:0:1D�6-   $ Normal��6G     5+      $ Pressed��6   $ 0:0:0.5D�6+   . 5-   
     
Normal��6     5.   
   	MouseOver��. 5/      �� .��50      �� $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6)   $ CommandLinkText��6v   .g�51        $ 0:0:0.5��6!   $ 	#FF074AE5��63   52      �� 53      $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6)   $ CommandLinkTextExtra��6v   .g�54        $ 0:0:0.5��6!   $ 	#FF074AE5��63   55      �� 56      $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6)   $ CommandLinkBorder��6r   .g�57        $ 0:0:0.5��6!   $ 	#FFC6C6C6��63   58      �� 59      $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6)   $ CommandLinkBorder��6�   .g�5:        $ 0:0:0.5��6!   $ 
WhiteSmoke��63   5;      �� 5<      $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�6)   $ CommandLinkBorder��6�   .g�5=        $ 0:0:0.5��6!   $ 
WhiteSmoke��63   5>      U� 5?      $ (UIElement.Opacity)�6*   $ CommandLinkIcon2��6Z   .\�5@      	  $ 0:0:0.5��6"   $ 1Q�64   5A      5B      5C        5D   
   Pressed��. 5E      �� .��5F      �� $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6)   $ CommandLinkBorder��6r   .g�5G        $ 0:0:0.25��6!   $ 	#FFADADAD��64   5H      �� 5I      $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6)   $ CommandLinkBorder��6�   .g�5J        $ 0:0:0.25��6!   $ 	#FFF2F3F3��64   5K      �� 5L      $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6)   $ CommandLinkBorder��6�   .g�5M        $ 0:0:0.25��6!   $ 	#FFF2F3F3��64   5N      �� 5O      $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�6)   $ CommandLinkBorder��6�   .g�5P        $ 0:0:0.25��6!   $ 	#FFF2F3F3��64   5Q      �� 5R      $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6)   $ CommandLinkText��6v   .g�5S        $ 0:0:0.25��6!   $ 	#FF062073��64   5T      �� 5U      $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6)   $ CommandLinkTextExtra��6v   .g�5V        $ 0:0:0.25��6!   $ 	#FF062073��64   5W      5X      5Y        5Z   
   Disabled��6   5[   
   ._�5]      � $ CommandLinkLayout��6    5^   	   ���� 5_   
   '��       " ��6   .�5`      ��  � ��$  0,4,9,0q�6$   �� 5a       � ��$  Stretch=�6/   5b      �� 5c   
   '��       " ��6   .�5d      ��  � ��$
  Wrap=�6*   �� 5e       � ��  �	#FF151C556(   5f      5g   
   ! 5h   	   �� 5i   
   $
��Auto �6   �� 5j   
   $��1* �6   " 5l   	   �� 5m   
   $
��Auto �6   �� 5n   
   $
��Auto �6   .+�5p   	   �� $# CommandLinkIcon��6   #���  6(   $ 5q   
   
  5r      $	% 190Q�6   $& 5Q�6,   $' 2Q�6;   $	( 0.4Q�6K   5t   
   �� 5u   	   $# CommandLinkIcon2��6   #���  6)   $) 0Q�6L   �� 5v   	   $* CommandLinkText��6   $��1��6,   $��16(�6<   �� 5w   
   ��6   �� *Text+$+ 1��8,   $ OneWay=�5x   
   �� 5y   	   $* CommandLinkTextExtra��6   $��1��61   $��1��6A   $��12(�6�   $��0,1,0,0q�67  ��6N   �� *Text+$+ 2��8,  $ OneWay=�y�6�   �� *Text+8,  $ OneWay=�67  5z   	   5{      - 5|      P� 5}      . �@J�	IsFocused/ . . �5~   	   �� $0 CommandLinkBorder��6    � ��  �	#FF7FE5FF6F   5   	   5�      5�      5�      5�      5�      