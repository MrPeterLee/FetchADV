   M S B A M L   `   `   `  ����C  ?PeterApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PeterAppPeterApp  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5          PeterApp.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:PeterApp     5      a ]DevExpress.Xpf.Core.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      d `DevExpress.Xpf.Docking.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxd5http://schemas.devexpress.com/winfx/2008/xaml/docking  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      a ]DevExpress.Xpf.Grid.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v14.1.Core, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 5
      Fcollections2clr-namespace:System.Collections;assembly=mscorlib  	 5      c
 _DevExpress.Xpf.NavBar.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxnb4http://schemas.devexpress.com/winfx/2008/xaml/navbar 
 5        � Title$  DXApplication��5      $	��700��6   $
��1100��6(    J� SnapsToDevicePixels .  � UseLayoutRounding . &   DevExpress.Xpf.Core.ThemeManager   	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$ 
Office2013 5       � Icon$ Images/Icons/Group.png��6,    � 	Resources 5      ��%    5      .��5      $  DevExpress.Xpf.Bars.BarManager  -   5        $��
barManager��5         CreateStandardLayout .    
Categories 5   
   ,  &DevExpress.Xpf.Bars.BarManagerCategory  5         Name$
 File��6%     5      $
 Edit��6%     5      $
 Help��6%     5      $
 View��6%     5      $ BuiltIn��6%   	   Items	 5   
   '  !DevExpress.Xpf.Bars.BarButtonItem  5      -   5        
 �Name$

 bNew��5       !  DevExpress.Xpf.Bars.BarItem   CategoryName$
 File��6,      Content)  #DevExpress.Xpf.Core.ObjectConverter$	 New 6@      Glyph$  Images/Icons/new-16x16.png��6N      
KeyGesture$ Ctrl+N��6q      Hint$ New Blank Document 6�     5      -   5        $
 bOpen��5       $
 File��6-   $
 Open 6A   $! Images/Icons/open-16x16.png��6P   $ Ctrl+O��6t   $
 Open 6�     5       -   5        $
 bClose��5        $
 File��6.   $ Close 6B   $" Images/Icons/close-16x16.png��6R   $ Close 6w     5!      -   5        $
 bSave��5!       $
 File��6-   $
 Save 6A   $! Images/Icons/save-16x16.png��6P   $ Ctrl+S��6t   $ Save Document 6�     5"      -   5        $
 bSaveAs��5"       $
 File��6/   $ 
Save As... 6C   $$ Images/Icons/save-as-16x16.png��6X   $ Save Document As... 6     5#      -   5        $
 bPrint��5#       $
 File��6.   $ Print 6B   $" Images/Icons/print-16x16.png��6R   $ Ctrl+P��6w     5$      -   5        $
 bExit��5$       $
 File��6-   $
 Exit 6A   $ Ctrl+X��6P     5&      -	   5        $
 bUndo��5&       $
 Edit��6-   $
 Undo 6A   $! Images/Icons/undo-16x16.png��6P   $ Ctrl+Z��6t     5'      -
   5        $
 bRedo��5'       $
 Edit��6-   $
 Redo 6A   $! Images/Icons/redo-16x16.png��6P   $ Ctrl+Y��6t     5(      -   5        $

 bCut��5(       $
 Edit��6,   $	 Cut 6@   $  Images/Icons/cut-16x16.png��6N   $ Ctrl+X��6q     5)      -   5        $
 bCopy��5)       $
 Edit��6-   $
 Copy 6A   $! Images/Icons/copy-16x16.png��6P   $ Ctrl+C��6t     5*      -   5        $
 bPaste��5*       $
 Edit��6.   $ Paste 6B   $" Images/Icons/paste-16x16.png��6R   $ Ctrl+V��6w     5+      -   5        $
 bClear��5+       $
 Edit��6.   $ Clear 6B     5,      -   5        $
 
bSelectAll��5,       $
 Edit��62   $ 
Select All 6F   $ Ctrl+A��6[     5.      -   5        $
 bPanels��5.       $
 View��6/   $ View ADV Bot Panel 6C   $ View ADV Bot Panel 6`   6z     5/      -   5        $
 bNavigation��5/       $
 View��63   $ View Navigation 6G   $ View Navigation Panel 6a   6~     50      -   5        $
 bTools��50       $
 View��6.   $ 
View Tools 6B   $ View Tools Panel 6W   6o     51      -   5        $
 bOutput��51       $
 View��6/   $ View Progress Output 6C   $  View Progress Output Panel 6b   6�     53      -   5        $
 bHome��53       $
 Help��6-   $ Visit Author's Homepage 6A     54      -   5        $
 bAbout��54       $
 Help��6.   $ About 6B   $  DevExpress.Xpf.Bars.BarSubItem  56      -   5        $
 smFile��56      $ BuiltIn��6+   $
 File 6B      	ItemLinks 57      +	  %DevExpress.Xpf.Bars.BarButtonItemLink	  58      %
  DevExpress.Xpf.Bars.BarItemLink 
  BarItemName$
 bNew��6,   	  59      $ bOpen��6,   	  5:      $ bClose��6,   .  (DevExpress.Xpf.Bars.BarItemLinkSeparator  5;      	  5<      $ bSave��6,   	  5=      $ bSaveAs��6,     5>      	  5?      $ bPrint��6,     5@      	  5A      $ bExit��6,   5C        5D      -   5        $
 smEdit��5D      $ BuiltIn��6+   $
 Edit 6B    5E      	  5F      $ bUndo��6,   	  5G      $ bRedo��6,     5H      	  5I      $
 bCut��6,   	  5J      $ bCopy��6,   	  5K      $ bPaste��6,     5L      	  5M      $ bClear��6,   	  5N      $ 
bSelectAll��6,   5P        5Q      -   5        $
 smView��5Q      $
 View��6+   $
 View 6?    5R      	  5S      $ bPanels��6,   	  5T      $ bNavigation��6,   	  5U      $ bTools��6,   	  5V      $ bOutput��6,   5X        5Y      -   5        $
 smHelp��5Y      $ BuiltIn��6+   $
 Help 6B    5Z      	  5[      $ bHome��6,     5\      	  5]      $ bAbout��6,   5_         Bars 5a   
     DevExpress.Xpf.Bars.Bar  5b      -   5          Name$ MainMenu��5b   )      Caption$ MainMenu��6      
IsMainMenu .    UseWholeRow] YDevExpress.Data.v14.1, Version=14.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$
 True 6M      DockInfo 5c      %  DevExpress.Xpf.Bars.BarDockInfo  5d         ContainerType$	 Top=�6&   /  )DevExpress.Xpf.Bars.BarItemLinkHolderBase   	ItemLinks 5f      (  "DevExpress.Xpf.Bars.BarSubItemLink  5g      $ smFile��6)     5h      $ smEdit��6)     5i      $ smView��6)     5j      $ smHelp��6)   5l        5m      $ Standard��6    5n        5o      $	 Top=�6&   
   Row$ 1��6:      Column$ 0��6B    5q      	  5r      $
 bNew��6,   	  5s      $ bOpen��6,   	  5t      $ bSave��6,     5u      	  5v      $ bPrint��6,     5w      	  5x      $
 bCut��6,   	  5y      $ bCopy��6,   	  5z      $ bPaste��6,     5{      	  5|      $ bUndo��6,   	  5}      $ bRedo��6,   5        5�      $ 
Status Bar��6      IsStatusBar . $
 True 6>    5�        5�      $ Bottom=�6&   5�      ._�5�   
   .  (DevExpress.Xpf.Docking.DockLayoutManager  -   5          Name$ dockLayoutManager��5�          
LayoutRoot. 5�      (  "DevExpress.Xpf.Docking.LayoutGroup     Items. 5�            Orientation$  Vertical=�6"   +  %DevExpress.Xpf.Docking.BaseLayoutItem!   	ItemWidth$! 3* �6:   . 5�      *  $DevExpress.Xpf.Docking.DocumentGroup  "   
ItemHeight$" 3* �6(   . 5�      *  $DevExpress.Xpf.Docking.DocumentPanel  -   5        #  Name$# dpPanel1��5�   ,   $   Caption$$ 	ADV Robot��6<   $" 3* �6P   (  "DevExpress.Xpf.Docking.ContentItem%   Content.% 5�      � $��10,0,0,0q�6#   & � RowDefinitions& 5�   "   �� 5�   &   $��20 �64   �� 5�   &   $��20 �64   �� 5�   &   $��20 �64   �� 5�   &   $��20 �64   �� 5�   &   $��20 �64   �� 5�   &   $��20 �64   �� 5�   &   $��30 �64   .+�5�   "   �� $��0��6(   $G��AData Source Folder: The path to the Name List of Companies (.xls)��65   �� 5�   "   $��1��6-   ' �� Orientation$' 
Horizontal=�6:   $��0,0,0,0q�6S   .+�5�   &   �� -   5        ( ��Name$( btnOpenSourcePath��5�   -   $��Choose Folder��6H   $	��100��6a   %  DevExpress.Xpf.Editors.TextEdit  5�   &   -   5        )  Name$) textboxSourcePath��5�   3   $	��300��6O   $��10,0,0,0q�6^   )  #DevExpress.Xpf.Editors.TextEditBase*   Text$!* D:\Workspace\SourceNameList��6p   5�   #   �� 5�   "   $��2��6(   $F��@Output Data Folder: Specify the dataset filename for ADV results��65   �� 5�   "   $��3��6-   $' 
Horizontal=�6:   $��0,0,0,0q�6S   .+�5�   &   �� -   5        $( btnOpenOutputPath��5�   -   $��Choose File��6H   $	��100��6_     5�   &   -    5        $) textboxOutputPath��5�   3   $#* D:\Workspace\S2AdvResults.xls��6N   $	��300��6s   $��10,0,0,0q�6�   5�   #   �� 5�   "   $��4��6(   $]��WOutput Data Folder: Specify the dataset filename for companies receiving NO ADV results��65   �� 5�   "   $��5��6-   $' 
Horizontal=�6:   $��0,0,0,0q�6S   .+�5�   &   �� -!   5        $( btnOpenOutputPath2��5�   -   $��Choose File��6I   $	��100��6`     5�   &   -"   5        $) textboxOutputPath2��5�   3   $ * D:\Workspace\S2AdvFail.xls��6O   $	��300��6q   $��10,0,0,0q�6�   5�   #   �� 5�   "   $��6��6-   $' 
Horizontal=�6:   $��Center=�6S   .+�5�   &   �� $��Start/Stop the App: ��6,   $��Center=�6K   $��Center=�6h   $��10,0,0,0q�6�   �� 5�   &   -#   5        $��btnStart��5�   -   $��Start BG Worker��6=   $
��10,3q�6W   $��Center=�6|   $��Center=�6�   �� 5�   &   -$   5        $��	btnStart2��5�   -   $��Start Temp Code��6>   $
��10,3q�6X   $��Center=�6~   $��Center=�6�   �� 5�   &   -%   5        $��	btnStart3��5�   -   $��Start Fetching��6>   $
��10,3q�6W   $��Center=�6}   $��Center=�6�   5�   #   5�      5�      5�      (  "DevExpress.Xpf.Docking.LayoutPanel  5�      -&   5        $# lpOutput��5�   &   $$ Progress Output��66   $" 3* �6P   .% 5�      � & 5�      �� 5�   "   $��* �60   �� 5�   "   $��20 �60   + � ColumnDefinitions+ 5�      �� 5�   "   $��* �63   �� 5�   "   $��0 �63   .+�5�      �� -'   5        $��tbOutput��5�   3   $��0��6&   $��0q�6C   ��. ", �� VerticalScrollBarVisibility$
, Auto=�6|   �� 5�      $��1��6)   $' 
Horizontal=�66   $��Right=�6O   $��0,0,10,0q�6k   .+�5�   "   �� $��
Progress: ��6(   $��Right=�6=   $��Center=�6Y   �� 5�   "   -(   5        $��	lProgress��5�   (   $��0%��69   $��Right=�6F   $��Center=�6b   5�      5�      5�      5�      (  "DevExpress.Xpf.Docking.TabbedGroup  5�      $! * �6"   -   
Visibility$- 	Collapsed=�60   . 5�        -)   5        $# lpProperties��5�   &   $$ 
Properties��6:     5�      -*   5        $# 
lpExplorer��5�   &   $$ Explorer��68   5�      5�      .   AutoHideGroups. 5�      *  $DevExpress.Xpf.Docking.AutoHideGroup  5�      . 5�        -+   5        $# lpNavigation��5�   &   $$ 
Navigation��6:   $	! 200 �6O   /   Padding$/ 1q�6_   .% 5�      ) 
 #DevExpress.Xpf.NavBar.NavBarControl  -,   5        $��navBarControl��5�   -   $��Stretch=�6B   $��Stretch=�6`   0   Groups0 5�      ' 
 !DevExpress.Xpf.NavBar.NavBarGroup  5�   "   1   Header$1 Group1 63   2   ImageSource$2 Images/Icons/Group.png��6C   3   Items.3 5�   &   & 
  DevExpress.Xpf.NavBar.NavBarItem  4   Content$4 Item1 66   5   ImageSource$5 Images/Icons/Item.png��6F     5�   &   $4 Item2 66   $5 Images/Icons/Item.png��6F     5�   &   $4 Item3 66   $5 Images/Icons/Item.png��6F     5�   &   $4 Item4 66   $5 Images/Icons/Item.png��6F   5�   #     5�   "   $1 Group2 63   $2 Images/Icons/Group.png��6C   .3 5�   &     $4 Item1 66   $5 Images/Icons/Item.png��6F     5�   &   $4 Item2 66   $5 Images/Icons/Item.png��6F     5�   &   $4 Item3 66   $5 Images/Icons/Item.png��6F     5�   &   $4 Item4 66   $5 Images/Icons/Item.png��6F     5�   &   $4 Item5 66   $5 Images/Icons/Item.png��6F   5�   #   6   View6 5�      + 
 %DevExpress.Xpf.NavBar.ExplorerBarView  5�   "   5�      5�        5�      --   5        $# lpTools��5�   &   $$ Tools��65   5�      5�      5�      5�      