   M S B A M L   `   `   `  ����C  ?S1Search, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:S1SearchS1Search  5          S1Search.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:S1Search     5      a ]DevExpress.Xpf.Core.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      d `DevExpress.Xpf.Docking.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxd5http://schemas.devexpress.com/winfx/2008/xaml/docking  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      j fDevExpress.Xpf.LayoutControl.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      a ]DevExpress.Xpf.Grid.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v14.1.Core, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 5	        � Title$B  <S2. ADV Download Robot Ver 1.0 by Chu, Juan and Peter (2014)��5      $	��768��6J   $
��1024��6W    J� SnapsToDevicePixels .  � UseLayoutRounding . &   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$ 
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
 5%      0  *DevExpress.Xpf.LayoutControl.LayoutControl   . .	  (DevExpress.Xpf.LayoutControl.LayoutGroup 	  View$ GroupBox=�6B   .+�5&      	   	  Header$ Settings��6+   $	��Top=�6=    . $ GroupBox=�6n   .+�5'      -
  'DevExpress.Xpf.LayoutControl.LayoutItem
   
  
IsRequired .  
  Label$F @Output Data Folder: Specify the dataset filename for ADV results��6@    
  LabelPosition$	 Top=�6�    
  Content. 5(   "   �� $ 
Horizontal=�6-   $��0,0,0,0q�6F   .+�5)   &   �� -   5        $ btnOpenOutputPath��5)   -   $��Choose File��6H   $	��100��6_   6n   %  DevExpress.Xpf.Editors.TextEdit  5*   &   -   5          Name$ textboxOutputPath��5*   3   )  #DevExpress.Xpf.Editors.TextEditBase   Text$# D:\Workspace\S2AdvResults.xls��6N   $	��300��6s   $��10,0,0,0q�6�   5+   #   5,      	  5-       	  Orientation$ Vertical=�6/   .+�5.   "   
   . $G AData Source Folder: The path to the Name List of Companies (.xls)��6D   $	 Top=�6�   . 5/   &   �� $ 
Horizontal=�61   $��0,0,0,0q�6J   .+�50   *   �� -   5        $ btnOpenSourcePath��50   1   $��Choose Folder��6L   $	��100��6e   6t     51   *   -	   5        $ textboxSourcePath��51   7   $	��300��6S   $��10,0,0,0q�6b   $! D:\Workspace\SourceNameList��6t   52   '   53   #   
  54   "    . $] WOutput Data Folder: Specify the dataset filename for companies receiving NO ADV results��6D   $	 Top=�6�   . 55   &   �� $ 
Horizontal=�61   $��0,0,0,0q�6J   .+�56   *   �� -
   5        $ btnOpenOutputPath2��56   1   $��Choose File��6M   $	��100��6d   6s     57   *   -   5        $ textboxOutputPath2��57   7   $ D:\Workspace\S2Output\��6S   $	��300��6q   $��10,0,0,0q�6�   58   '   59   #   5:      5;      
  5<      $
��Left=�6*   $	��Top=�6E   $��Center=�6]   . 5=      &   DevExpress.Xpf.Editors.ImageEdit  %  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly . $	��150��6>   $��Center=�6J   $��Center=�6g   5>      5?      5A        5B      $$ Program Output (Bottom to Top)��6"   $ 2.38* �6K   .
 5C      �  � ColumnDefinitions 5D      �� 5E      $��* �6/   �� 5F      $��0 �6/   .+�5H      �� -   5         ��Name$ 	TxtOutput��5H   "   $��5q�5I      $��Consolas)�5J      $��30��5K      
�������5L      5M      5N      5O      5P      5Q      5R      