   M S B A M L   `   `   `  ����C  ?S1Search, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:S1SearchS1Search  5          S1Search.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:S1Search     5      a ]DevExpress.Xpf.Core.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      d `DevExpress.Xpf.Docking.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxd5http://schemas.devexpress.com/winfx/2008/xaml/docking  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      j fDevExpress.Xpf.LayoutControl.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      a ]DevExpress.Xpf.Grid.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v14.1.Core, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 5	        � Title$<  6ADV Search Robot Ver 1.0 by Chu, Juan and Peter (2014)��5      $	��768��6D   $
��1024��6Q    J� SnapsToDevicePixels .  � UseLayoutRounding . &   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$ 
Office2013 5      .��5      .  (DevExpress.Xpf.Docking.DockLayoutManager  -   5          Name$ dockLayoutManager��5         
LayoutRoot. 5   
   (  "DevExpress.Xpf.Docking.LayoutGroup     Items. 5      (  "DevExpress.Xpf.Docking.LayoutPanel  +  %DevExpress.Xpf.Docking.BaseLayoutItem   	ItemWidth$	 200 �6      Caption$ Download Status��6.   	   Padding$	 1q�6H   (  "DevExpress.Xpf.Docking.ContentItem
   Content.
 5      � -   5         �Name$ 
LayoutRoot��5      
�������.+�5      �� $��30��6!    �� Orientation$ 
Horizontal=�6-   $
��Left=�5      $	��Top=�60   $��10q�5      .+�5      �� -   5         ��Name$ buttonStart��5   !   $��Start��66   $��80��5      $��30��6    �� 5      -   5        $ buttonCancel��5   !   $��Cancel��67   $��80��5      $��30��6    5      �� 5      $��	10,50,0,0q�6!   $ 
Horizontal=�64   .+�5      �� $��
Progress: ��6$   �� 5      -   5         ��Name$ 
tbProgress��5   $   5      5       5!        5"         Orientation$ Vertical=�6   $ 4* �65   . 5#        $ MainView��6"      
ItemHeight$
 1.2* �65   .
 5%      0  *DevExpress.Xpf.LayoutControl.LayoutControl  .	  (DevExpress.Xpf.LayoutControl.LayoutGroup 	  Orientation$ Vertical=�6)    .  	  View$ GroupBox=�6Y   .+�5&      	  .+�5'      	   	  Header$ Settings��6/   $ Vertical=�6A   $	��Top=�6X    . $ GroupBox=�6�   .+�5(   "   -
  'DevExpress.Xpf.LayoutControl.LayoutItem
   
  
IsRequired .  
  Label$G AData Source Folder: The path to the Name List of Companies (.xls)��6D    
  LabelPosition$	 Top=�6�    
  Content. 5)   &   �� $ 
Horizontal=�61   $��0,0,0,0q�6J   .+�5*   *   �� -   5        $ btnOpenSourcePath��5*   1   $��Choose Folder��6L   $	��100��6e   6t   %  DevExpress.Xpf.Editors.TextEdit  5+   *   -   5          Name$ textboxSourcePath��5+   7   $	��300��6S   $��10,0,0,0q�6b   )  #DevExpress.Xpf.Editors.TextEditBase   Text$! D:\Workspace\SourceNameList��6t   5,   '   5-   #   
  5.   "    . $F @Output Data Folder: Specify the dataset filename for ADV results��6D   $	 Top=�6�   . 5/   &   �� $ 
Horizontal=�61   $��0,0,0,0q�6J   .+�50   *   �� -   5        $ btnOpenOutputPath��50   1   $��Choose File��6L   $	��100��6c   6r     51   *   -	   5        $ textboxOutputPath��51   7   $% D:\Workspace\13FAdvNameList.xls��6R   $	��300��6y   $��10,0,0,0q�6�   52   '   53   #   
  54   "    . $] WOutput Data Folder: Specify the dataset filename for companies receiving NO ADV results��6D   $	 Top=�6�   . 55   &   �� $ 
Horizontal=�61   $��0,0,0,0q�6J   .+�56   *   �� -
   5        $ btnOpenOutputPath2��56   1   $��Choose File��6M   $	��100��6d   6s     57   *   -   5        $ textboxOutputPath2��57   7   $) #D:\Workspace\13FAdvNoResultList.xls��6S   $	��300��6~   $��10,0,0,0q�6�   58   '   59   #   5:      
  5;      $��Right=�6.   $	��Top=�6J   $��Center=�6b   . 5<   &   &   DevExpress.Xpf.Editors.ImageEdit  %  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly . $	��150��6F   $��Center=�6R   $��Center=�6o   5=      5>      5?      5A        5B      $$ Program Output (Bottom to Top)��6"   $ 2.38* �6K   .
 5C      �  � ColumnDefinitions 5D      �� 5E      $��* �6/   �� 5F      $��0 �6/   .+�5H      �� -   5         ��Name$ 	TxtOutput��5H   "   $��5q�5I      $��Consolas)�5J      $��30��5K      
�������5L      5M      5N      5O      5P      5Q      5R      