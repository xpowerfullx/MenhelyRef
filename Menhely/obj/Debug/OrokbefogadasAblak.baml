   M S B A M L   `   `   `  ����  Menhely     Menhely.OrokbefogadasAblak   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$+  %Örökbefogadási kérelmek kezelése��5   	   $	��342��62   $	��673��6?    � 
ResizeMode$ CanMinimize=�6K    � WindowStartupLocation$ CenterScreen=�6d   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	allatokDG��5      $
��Left=�6p   $��	10,36,0,0q�6�   $	��Top=�6�   $	��240��6�   $	��645��6�      
IsReadOnly .    AutoGenerateColumns .  ��6&   �� *	Allatok+ �� SelectedItem 6F   �� *KivalsztottAllat+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Állat neve��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 69   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6E   �� *Nev+  5	      $
 Faja��6%   $	 80 63   
 6>   �� *Faj+  5
      $ Alfaja��6%   $	 80 65   
 6@   �� *AlFaj+  5      $
 Kora��6%   $	 40 63   
 6>   �� *Kor+  5      $ Örökbeadási állapot��6%   $		 130 6B   
 6N   �� *	Allapot+  5      $ Örökbefogadó��6%   $		 110 6;   
 6G   �� *Orokbefogado.Nev+5      �� 5   
   -   5        $��	Elfogadas��5      $
��Left=�6%   $��491,281,0,0q�6@   $	��Top=�6U   $	��164��6m   6y   �� 5   
   -   5        $��Visszautasítás��5      $
��Left=�6*   $��322,281,0,0q�6E   $	��Top=�6Z   $	��164��6r   6~   �� 5   
   -   5        $��Mégse��5      $
��Left=�6!   $��228,281,0,0q�6<   $	��Top=�6Q   $��89��6i    �� IsCancel . 6�   �� 5   
   $[��UÖrökbeadási kérelmeket az alábbi ön által gondozott állatokra nyújtottak be:��6   $
��Left=�6g   $��	10,10,0,0q�6�   $	��Top=�6�   5      5      