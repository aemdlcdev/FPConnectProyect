   M S B A M L   `   `   `  ���� D  @FPConnect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FPConnect.viewFPConnect.view      FPConnect.view.Login   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962ac _MaterialDesignThemes.MahApps, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   *localclr-namespace:FPConnect.view     � Title$  Login��$	��450��$	��800�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � 	Resources    FadeInStoryboard �� TargetProperty W� From	 W� To g� Duration��%A   &	        �� .��W� $ Opacity�$ 0X�$ 1X�$ 0:0:1D�	 � Triggers	 :� 
 :� RoutedEvent$
 Window.Loaded��.[��� #��[  .��� -   ����  �� 
StartPoint$ 0.1,0&� �� EndPoint$ 0.9,1&�.���  � Color$ #A7C7E7�� � Offset$ 1Q�� $ White��$ 0Q�.+��� -   $	��390��$	��Top=� �� CornerRadius$ 	0 0 180 0�����#F5F5F5��  �� Orientation$ 
Horizontal=�.+��� $	��350��.+��� $	��300��$	��300��$	��Top=�$��30q�$
��Fill=�$��/Images/ICON4801.png���� $	��350��.+��� $��20 40q�.+��� $��Iniciar Sesión��$��20q�
���   � �� TextAlignment$ Center=�$��38(�$��Great Vibes)��� $ 
Horizontal=�$��10q�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�$��25��$��25��
���   ��� -    ��Name$ txtUsername��$
��10 0q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$" Introduce el usuario o email��
���   �$	��250��
���   � �� 
CaretBrush �	#FFD94448 �� SelectionBrush �	#FFD94448�� $ 
Horizontal=�$��10q�.+�  $
 Lock=�$��25��$��25��
���   �G� -    G�Name$ txtPassword��$
��10 0q�$ ********��
���   �$	��250��
���   � G� 
CaretBrush �	#FFD94448 G� SelectionBrush �	#FFD94448�� $��10q�$��Center=�.+��� -    ��Name$ btnLogin��$	��100��$��40��  MaterialDesignRaisedButton#��[ +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��$��2q�
���   ����#F5F5F5
���   � � ToolTip$ Login��$��Login���� $	��100��.+��� -   $ btnExit��$��10 20q� & "MaterialDesignFloatingActionButton#��[ $ Close��
���   ���Z� .��  $ Close=�
���   �$��30��$��30��