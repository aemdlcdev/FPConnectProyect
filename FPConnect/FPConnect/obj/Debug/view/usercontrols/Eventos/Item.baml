   M S B A M L   `   `   `  ����D  @FPConnect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:FPConnect.view.UserControls.Eventos#FPConnect.view.UserControls.Eventos  5      .    (FPConnect.view.UserControls.Eventos.Item   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      =local1clr-namespace:FPConnect.view.UserControls.Eventos  5      -   5        $
��item��5      .��5
      � $��30 10q�6     � ColumnDefinitions  5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   "  FontAwesome.WPF.ImageAwesome  $��20��6F   $��20��6Q   $	��Top=�6]      
Foreground �#f1f1f1$��0 3 0 0q�6�      Icon 6   ��  �� Path$
 Icon� �� ElementName$
 item��6�   �� 5   
   $��1��6   $
��10 0q�6%   .+�5      �� $��16(�6   ��6&   �� $ Color�$
 item����6Y   �� $ Title�$
 item���� 5       �� Orientation$ 
Horizontal=�6   $��0 5 0 0q�62   .+�5        $��2��6"   $��12��6b   $��12��6m    62   �� $ IconBell�$
 item�� 6y   �� $ Color�$
 item���� 5      $��12(�6   $��8 0 0 0q�6�   ��6*   �� $ Color�$
 item����6]   �� $
 Time�$
 item��6�   5      5      d� 5   
   -   5        $��btnMenu��5      $��2��6&   ��5      �� 5       �� 
TargetType" d�6"   .R�5         $ 	EllipsisV=�6&   $��16��67   $��16��6B    �#f1f1f1��5!      �� 5"       �� 
TargetType"  6$    �� Triggers 5#   "   P� 5$   &   	 P� Property	 �@J�IsMouseOver
 P� Value
 . . �5%   *   ��  �� Property � z� �� Value 5&   .   �� 5'   2    �� ScaleX$	 1.2Q�6A    �� ScaleY$	 1.2Q�6N   5)   +   5*   '   5,      5.      5/      51      � 53   
    � AllowsTransparency .  � 	StaysOpen .  $
��Left=�6�    � VerticalOffset$ 30��6�    � HorizontalOffset$ 15��6�   ��6   �� *	IsChecked+$ btnMenu�� � PlacementTarget 6n   �� $ btnMenu��.��54      �� 
������� �� CornerRadius$ 5��6(    �� Padding$	 0 4q�69   ._�55      �� .+�57      4   .FPConnect.view.UserControls.Eventos.MenuButton  -   5          Name$ btnHecho��57   '      Icon$ Check=�69      Caption$ Hecho��6F     58      -   5        $ 	btnBorrar��58   '   $ Trash=�6:   $ Borrar��6G   5;      5<      5=      5?      5@      