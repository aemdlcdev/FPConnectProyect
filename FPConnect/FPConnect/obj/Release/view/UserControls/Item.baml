   M S B A M L   `   `   `  ���� D  @FPConnect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Calender.UserControlsCalender.UserControls       Calender.UserControls.Item   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  /local#clr-namespace:Calender.UserControls  -   $
��item��.��� $��30 10q�  � ColumnDefinitions  �� $
��auto ��� $��* ��� $
��auto �.+�"  FontAwesome.WPF.ImageAwesome  $��20��$��20��$	��Top=�   
Foreground �#f1f1f1$��0 3 0 0q�   Icon ��  �� Path$
 Icon� �� ElementName$
 item���� $��1��$
��10 0q�.+��� $��16(����� $ Color�$
 item������ $ Title�$
 item����  �� Orientation$ 
Horizontal=�$��0 5 0 0q�.+�  $��2��$��12��$��12�� �� $ IconBell�$
 item�� �� $ Color�$
 item���� $��12(�$��8 0 0 0q����� $ Color�$
 item������ $
 Time�$
 item��d� -   $��btnMenu��$��2������  �� 
TargetType" d�.R�  $ 	EllipsisV=�$��16��$��16�� �#f1f1f1����  �� 
TargetType"   �� Triggers P� 	 P� Property	 �@J�IsMouseOver
 P� Value
 . . ���  �� Property � z� �� Value ��  �� ScaleX$	 1.2Q� �� ScaleY$	 1.2Q��  � AllowsTransparency .  � 	StaysOpen .  $
��Left=� � VerticalOffset$ 30�� � HorizontalOffset$ 15������ *	IsChecked+$ btnMenu�� � PlacementTarget �� $ btnMenu��.���� 
������� �� CornerRadius$ 5�� �� Padding$	 0 4q�._��� .+�&    Calender.UserControls.MenuButton     Icon$ Check=�   Caption$ Check��  $ 	BellSlash=�$ 	Silenciar��  $ Pencil=�$ Editar��  $ Trash=�$ Borrar��