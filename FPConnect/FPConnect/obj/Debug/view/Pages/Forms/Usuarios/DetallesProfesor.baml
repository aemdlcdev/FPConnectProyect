   M S B A M L   `   `   `  ����D  @FPConnect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:FPConnect.view.Pages.Forms.Usuarios#FPConnect.view.Pages.Forms.Usuarios  5   	   :    4FPConnect.view.Pages.Forms.Usuarios.DetallesProfesor   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _MaterialDesignThemes.MahApps, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	   ?local1clr-namespace:FPConnect.view.Pages.Forms.Usuarios   5   	     � Title$  DetallesProfesor��5	   	    � WindowStartupLocation$ CenterScreen=�6"    � WindowStyle$
 None=�6G    � AllowsTransparency . ���	#80000000 � WindowState$ 	Maximized=�5
   :    � Topmost . .��5      � $��Center=�6   $��Center=�6&   .+�5   
   �� ���#F7F6F4
���   �$��2q�6:    �� Padding$ 20q�6N    �� CornerRadius$ 10��5      $	��500��6#   ._�5      �� $
��Auto=�6   $	��600��6>   .��5      �� .+�5      �� $��Detalles del Profesor��6       	titleText#��[  6=   �� 5      $��Nombre:��6    $��0 10 0 0q�6/   $
��Bold"�6A   �� 5      -   5         ��Name$ 	txtNombre��5      $	��0,5q�6V     textBoxNormal#��[ 6c   $	��450��6�   	 �� 
IsReadOnly	 . ��61   �� *nombre+
 �� Mode$
 OneWay=�6�   �� 5      $��
Apellidos:��6    $
��Bold"�62   �� 5      -   5        $ txtApellidos��5      $	��0,5q�6\   #��[ 6i   $	��450��6�   	 . ��64   �� *	apellidos+$
 OneWay=�6�   �� 5      $��Correo Electrónico:��6    $
��Bold"�6;   �� 5      -   5        $ txtEmail��5      $	��0,5q�6T   #��[ 6a   $	��450��6�   	 . ��60   �� *email+$
 OneWay=�6�   �� 5      $��Sexo:��6    $
��Bold"�6-   �� 5      -   5        $ txtSexo��5      $	��0,5q�6R   #��[ 6_   $	��450��6�   	 . ��6/   �� *sexo+$
 OneWay=�6�   �� 5       $��Departamento:��6    $
��Bold"�65   �� 5!      -   5        $ txtDepartamento��5!      $	��0,5q�6i   #��[ 6v   $	��450��6�   	 . ��67   �� *nombre_departamento+$
 OneWay=�6�   �� 5#      $��Curso:��6    $
��Bold"�6.   �� 5$      -   5        $ txtCurso��5$      $	��0,5q�60   #��[ 6=   $	��450��6d   	 . 6p   �� 5&      $
��Rol:��6    $
��Bold"�6,   �� 5'      -   5        $ txtRol��5'      $	��0,5q�6.   #��[ 6;   $	��450��6b   	 . 6n   �� 5)      $��Centro:��6    $
��Bold"�6/   �� 5*      -   5        $ 	txtCentro��5*      $	��0,5q�61   #��[ 6>   $	��450��6e   	 . 6q   �� 5,      $��Turno:��6    $
��Bold"�6.   �� 5-      -	   5        $ txtTurno��5-      $	��0,5q�60   #��[ 6=   $	��450��6d   	 . 6p   �� 50       �� Orientation$ 
Horizontal=�6!   $��0,10,0,5q�6:   .+�51      �� $��Estado:��6$   $
��Bold"�63   $��Center=�6E   A� 52      -
   5         A�Name$ estadoIndicator��52   "   $��15��6;   $��15��6F   $��10,0,0,0q�6R   $��Center=�53   ?   ��6"   �� *EstadoColor+6?   �� 54      -   5         ��Name$ 	txtEstado��54   $   $��10,0,0,0q�67   $��Center=�6I   55      �� 58      $��Color asignado:��6    $
��Bold"�67   $��0,5,0,5q�6I   �� 59      $ 
Horizontal=�6!   $��0,0,0,10q�6:   .+�5:      �� -   5         ��Name$ borderColor��5:   !   $��30��66   $��30��6A   $ 15��6M   ��5;   !   �� *	bgColor+._�5<      �� -   5        $ txtFirstChar��5<   (   $��Center=�5=   (   $��Center=�6E   $
��Bold"�5>   (   
���������5<   >   �� *	character+5>   :   5?      5@      �� 5C      -   5        $��Cerrar��5C      $��80��6.   $��0,20,0,0q�69   $��Right=�6K     buttonMainGestionUsuarios#��[ 5D   2   $��30��6e   5E      5F      5G      5H      5I      