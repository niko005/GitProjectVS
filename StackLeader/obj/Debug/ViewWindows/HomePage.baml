   M S B A M L   `   `   `  ����F  BStackLeader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:StackLeader.ViewWindowsStackLeader.ViewWindows  5      &     StackLeader.ViewWindows.HomePage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:StackLeader.ViewWindows     5        N� Title$  HomePage��5	       N� 
Background
 ����  � 	Resources 5         
menuButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� Opacity	 �� Triggers
 P� Property P� Value �� 
TargetName��%U  &	        �� 5   
   " ��6#   .�5      ��  � �� �#4946466+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� #��z��6%   $ 10��6O   ._�5   "   �� $��Center=�63   $��Center=�6P   5      �� 5      $��hoverOverlay��6X   ���#1f1f1f$ 10��6:   $ 0Q�6L   6X   5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   �� $ hoverOverlay��6)    �@J�Opacity$	 0.2Q�6V   5       P� 5!      
 � �� . . �5"   "   �� $ hoverOverlay��6)    �@J�Opacity$	 0.4Q�6V   5#      5%      5'      5(      5)      .��5+      �� -   5         ��Name$ 
MainBorder��5+      ���#CCCCCC$ 	0,0,20,20��66   ._�5,   
   �  � ColumnDefinitions 5-      �� 5.      $	��200 �6#   �� 5/      $��* �6#   .+�52      �� ���#4d4d4d$��0��6*   $ 	0,50,0,20��6:   ._�53      ]� .+�54      �� $
��Left=�6!   $	��Top=�6<   $��	20,20,0,0q�6T   .+�55      �� $��0,0,0,10q�6%   .+�56      �� -   5        #��[  56   %    � Cursor$
 Hand��6I   .��57   "   ��  �� Orientation$ 
Horizontal=�6-   .+�58   &   �� $%��Список поручений��60   $��14(�6H    �� Padding$ 5q�6V   6d   59   #   5:      5;      �� 5<      $��0,0,0,10q�6%   .+�5=      �� -   5        #��[  5=   %   $
 Hand��6I   .��5>   "   �� $ 
Horizontal=�6-   .+�5?   &   �� $)��#Добавить поручение��60   $��14(�6J   $ 5q�6X   6f   5@   #   5A      5B      �� 5C      $��0,0,0,10q�6%   .+�5D      �� -   5        #��[  5D   %   $
 Hand��6I   .��5E   "   �� $ 
Horizontal=�6-   .+�5F   &   �� $��
Отчет��60   $��14(�6=   $ 5q�6K   6Y   5G   #   5H      5I      5J      5K      5L      5M      5N      5O      