   M S B A M L   `   `   `  ����R  NIExtendFramework, Version=1.0.4679.22611, Culture=neutral, PublicKeyToken=nullk:clr-namespace:IExtendFramework.Controls.AdvancedMessageBox,IExtendFramework.Controls.AdvancedMessageBox  5      =    7IExtendFramework.Controls.AdvancedMessageBox.TaskDialog   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Llocal:clr-namespace:IExtendFramework.Controls.AdvancedMessageBox     5        � 
ResizeMode$  NoResize=�5       � ShowInTaskbar .   � SizeToContent$ WidthAndHeight=�5      $	��500��5      $	��375��5       J� SnapsToDevicePixels . &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5       � Title 5      �� *Title+ �� Mode$ OneWay=� � 	Resources 5       �� MergedDictionaries	 �� Source   BooleanVisibilityConverterM  GIExtendFramework.Controls.AdvancedMessageBox.NotNullVisibilityConverter  NotNullVisibilityConverter  	ExpArrUp1
 �� 	UriSource  	ExpArrUp2  	ExpArrUp3  ExpArrDown1  ExpArrDown2  ExpArrDown3  ExpArr1 	 ExpArr2 
 ExpArr3�� 5       5      �� 5      $&	  ResourceDictionaries/Common.xaml?�6   %b  &	        &	      &	 &     &	 X     &	 �     &	 �     &	 �     &	 $    &	 X    &		 �    &	
 �    �� 5      6"     5      6&   �� 5      $
 Resources/exparr-up1.bmp?�6#   �� 5      $
 Resources/exparr-up2.bmp?�6#   �� 5      $
 Resources/exparr-up3.bmp?�6#   �� 5      $ 
 Resources/exparr-down1.bmp?�6%   �� 5      $ 
 Resources/exparr-down2.bmp?�6%   �� 5      $ 
 Resources/exparr-down3.bmp?�6%   �� 5       $ 
 Resources/exparr-down1.bmp?�6!   �� 5!      $ 
 Resources/exparr-down2.bmp?�6!   �� 5"      $ 
 Resources/exparr-down3.bmp?�6!   5#      .��5%      � -   5         �Name$ 
LayoutRoot��5%       � RowDefinitions 5&      �� 5'      $
��Auto �6   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   .+�5+      �� -   5         ��Name$ ContentArea��5+       �� Padding$ 7,3,0,7q�6    ._�5,      �  � ColumnDefinitions 5-      �� 5.      $
��Auto �6   �� 5/      $��1* �6   .+�51      �� -   5         ��Name$ MainIcon��51      $��3,7,7,8q�6G   $	��Top=�6X   $��32��6p   $��32��6{   ��6   �� *
MainIcon+$ OneWay=�y�6�   �� *
MainIcon+ �� 	Converter# [ $ OneWay=�� 52      $��1��6    53       �� 
TargetType �� Property �� Value��%T   '��       �� 54      " ��6   .�55   	   ��  � ��$
 Wrap=�6(   56   	   57       58      �� 59      $
��Auto �6   �� 5:      $
��Auto �6   �� 5;      $
��Auto �6   �� 5<      $
��Auto �6   .+�5>      �� -   5         ��Name$ MainInstruction��5>      $��16(�6X   $��3,7,5,3q�6f   ���	#FF0033B3��6*   �� *MainInstruction+$ OneWay=�y�6�   �� *MainInstruction+# [ $ OneWay=��� 5?      -   5        $ ContentText��5?      $��1��6&   $��3,12,3,8q�6Y   ��63   �� *	Content+$ OneWay=�y�6k   �� *	Content+# [ $ OneWay=��� 5@      -   5        $ ExpandedInfo��5@      $��2��6'   $��3,0,3,8q�6_   ��64   �� *ExpandedInfo+$ OneWay=�y�6p   �� *	IsChecked+# [   �� ElementName$ ShowDetailsButton��$ OneWay=��� 5A      -   5         ��Name$ CommandLinks��5A      $��3��6*   $��	4,17,11,3q�67     ItemsPanelTemplate#���  6o     DataTemplate#���  6�   ��6J   �� *CommandLinks+ 5B      >   8IExtendFramework.Controls.AdvancedMessageBox.CommandLink>   8IExtendFramework.Controls.AdvancedMessageBox.CommandLink   Click��%   &	       &	 (     �� 5C   	   .R�5D   
   �� 5E   
   �� 5F   	   .R�5G   
      CommandLink_Click6   5H   
   5I   	   5J      �� 5K      -   5        $ RadioButtons��5K      $��3��6*   $��
10,15,10,6q�67   #���  6p   #���  6�   ��6K   �� *RadioButtons+ 5L       	� 	GroupName d� 	IsChecked��%   &	       &	 (     �� 5M   	   .R�5N   
   �� 5O   
   �� 5P   	   .R�5Q   
   	� $��3,3,3,4q�6   $ default��6'   ��6;   �� *Text+$ OneWay=� 6a   �� *	IsDefault+$ OneWay=���6�   �� *	Command+$ OneWay=���6�   �� *ID+$ OneWay=�5R   
   5S   	   5T      5U      5V      5W      �� 5X      -	   5        $ ButtonsArea��5X      $��1��6    $��0,1,0,0q�6-   $	 9,6q�6G   #��� ��6U   #��� ��6�   ._�5Y      �  5Z      �� 5[      $
��Auto �6   �� 5\      $��1* �6   .+�5^      ��  �� Orientation$ Vertical=�6   .+�5_      d� -
   5         d�Name$ ShowDetailsButton��5_      $��1,3,0,3q�6/   $
��Left=�6�     ButtonControlTemplate#���  6�   y�6@   �� *ExpandedInfo+# [ $ OneWay=� 5`       �� 
TargetType � UseLayoutRounding �� BitmapScalingMode  �� Triggers! ]� 
Conditions" �� Property# �� Value$ �� 
TargetName% P� Property& P� Value��%�  &	       �� 5a   	   " d�67   .R�5b   
   �� $ 
Horizontal=�6   
������ .+�5c      �� $ ShowDetailsArrow��6   #���  6+   $
��None=�6R   $��19��6a   $��19��6l   $��Center=�6x   $
��Left=�6�    . $ NearestNeighbor=�6�   �� 5d      $ ShowDetailsLabel��6   $��Show details��6/   $��8,0,0,0q�6C   $
��Left=�6T   $��Center=�6o   5e        5f   
   ]� 5g      ! 5h      �� 5i      " �@d�	IsChecked$# FalseY�6,   �� 5j      " �@J�IsMouseOver# . 6.   ./�5l      �� $$ ShowDetailsArrow��6   # �  6C    � ��5m      ]� 5n      ! 5o      �� 5p      " �@d�	IsChecked$# FalseY�6,   �� 5q      " � ��# . 6,   ./�5s      �� $$ ShowDetailsArrow��6   # �  6C    � ��5t      P� 5u      % �@d�	IsChecked$
& TrueY�6(   . �5v      �� $$ ShowDetailsArrow��6   # �  6C    � ���� 5w      $$ ShowDetailsLabel��6    � ��$ Hide details��6A   5x      P� 5y      % �@d�	IsChecked$& FalseY�6(   . �5z      �� $$ ShowDetailsLabel��6    � ��$ Show details��6A   5{      ]� 5|      ! 5}      �� 5~      " �@d�	IsChecked$
# TrueY�6,   �� 5      " �@J�IsMouseOver# . 6.   ./�5�      �� $$ ShowDetailsArrow��6   # �  6C    � ��5�      ]� 5�      ! 5�      �� 5�      " �@d�	IsChecked$
# TrueY�6,   �� 5�      " � ��# . 6,   ./�5�      �� $$ ShowDetailsArrow��6   # �  6C    � ��5�      5�   
   5�   	   5�      �� 5�      -   5        ' ��Name$' VerificationCheckBox��5�      $	��4,3q�6.   $
��Left=�6�   $	��215��6�   y�6;   �� *VerificationText+# [ $ OneWay=� 6�   �� *VerificationChecked+$ TwoWay=���5�      �� 5�   	   $
��Wrap=�6B   $	��Top=�6V   $��7,0,0,0q�6n   ��6   �� *VerificationText+$ OneWay=�6n   5�      5�      �� 5�      -   5        $ Buttons��5�      $��1��6$     ButtonsItemsPanelTemplate#���  6Z     ButtonsDataTemplate#���  6�   ��64   �� *NormalButtons+ 5�      ( � Orientation) �� IsCancel* �� 	IsDefault��%�  &	       &	 O     �� 5�      .R�5�   	   � $( 
Horizontal=�6   $��Right=�6,   5�   	   �� 5�      .R�5�   	   �� -   5        $	��4,3q�5�      $
��12,1q�6   $��66��6,   $��21��6:   ��6I   �� *Text+��6b   �� *	Command+$ OneWay=���6�   �� *ID+$ OneWay=�) 6�   �� *
IsCancel+$ OneWay=�* 6�   �� *	IsDefault+$ OneWay=�6  5�   	   5�      5�      5�      5�      �� 5�      -   5        $ 
FooterArea��5�      $��2��6   $��0,1,0,0q�6,   #��� ��6�   #��� ��6�   y�6F   �� *
FooterText+# [ $ OneWay=�._�5�      �� -   5        $ FooterInner��5�      $��0,1,0,0q�6!   $ 9q�6;   #��� ��6G   ._�5�      �  5�      �� 5�      $
��Auto �6   �� 5�      $��1* �6   .+�5�      �� -   5        $ 
FooterIcon��5�      $	��Top=�6L   $��16��6d   $��16��6o   $	��3,0q�6{   ��6!   �� *
FooterIcon+$ OneWay=�6{   �� 5�      -   5        $ 
FooterText��5�      $��1��6N   $	��4,0q�6^   ��6%   �� *
FooterText+$ OneWay=�6^   5�      5�      5�      5�      5�      