   M S B A M L   `   `   `  ����  Admin_Client    Admin_Client.FoAblak   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$*  $Cicabarát menhely Admin alkalmazás��5   	   $	��520��64   $
��1000��6A    � 
ResizeMode$ CanMinimize=�6N    � WindowStartupLocation$ CenterScreen=�6g   .��5      � .+�5   
   �� $
��Left=�6   $	��113��6+   $��	10,24,0,0q�68   $	��Top=�6K   $	��238��6c   $��Resources/logo.png��6o    �� StretchDirection$ DownOnly=�6�   �� 5   
   -   5         ��Name$ 	gombokTab��5      $
��Left=�6(   $	��274��6C   $��
10,150,0,0q�6P   $	��Top=�6d   $	��223��6|   .@�5      �� $��	Telephely��6   .��5	      � ���	#FFE5E5E5.+�5
      �� $��Adatok kezelése��6   $��10q�68   .��5      �� $
��Left=�6%   $	��187��6@   $��4,16,0,0q�6M   $	��Top=�6_   $	��178��6w   .+�5      �� -   5         ��Name$ TelephelyFelveteleButton��5   %   $��Telephely felvétele��6G   $��30��6e   6q   �� 5      -   5        $" TelephelyMegszunteteseButton��5   %   $��Telephely megszüntetése��6o   $��30��6�   {�6K   �� *LockTelephely+6�   �� 5      -   5        $ KetrecFelveteleButton��5   %   $��Ketrec felvétele��6h   $��30��6�   {�6D   �� *LockTelephely+6�   �� 5      -   5        $ KetrecModositasaButton��5   %   $��Ketrec módosítása��6f   $��30��6�   {�6E   �� *
LockKetrec+6�   �� 5      -   5        $ KetrecTorleseButton��5   %   $��Ketrec törlése��6c   $��30��6|   {�6B   �� *
LockKetrec+6�   �� 5      -   5        $ AllatAthelyezeseButton��5   %   $��Állat áthelyezése��6f   $��30��6�   {�6E   �� *
LockKetrec+6�   5      5      5      5      �� 5      $��Gondozó��6   .��5      � ���	#FFE5E5E5.+�5      �� $��Adatok kezelése��6   $��
10,10,10,0q�68   $	��226��6L   $	��Top=�6Y   .��5      �� $
��Left=�6%   $	��152��6@   $��4,16,0,0q�6M   $	��Top=�6_   $	��178��6w   .+�5      �� -	   5        $ GondozoFelveteleButton��5   %   $��Gondozó felvétele��6E   $��30��6a   6m   �� 5      -
   5        $ GondozoModositasaButton��5   %   $��Gondozó módosítása >>��6F   $��30��6f   6r   �� 5      -   5        $ GondozoTorleseButton��5   %   $��Gondozó törlése��6C   $��30��6]   6i   �� 5      -   5        $ AllatmodositasaButton��5   %   $��Állatok módosítása��6d   $��30��6�   {�6D   �� *	LockAllat+6�   �� 5      -   5        $ MunkahelymodositasaButton��5   %   $��Munkahelyek módosítása��6h   $��30��6�   {�6H   �� *	LockAllat+6�   5      5       5!      5"      5$      �� 5%   
   -   5        $ KilepesButton��5%      $��	Kilépés��6(   $
��Left=�6:   $��
56,435,0,0q�6U   $	��Top=�6i   $	��122��6�   $��29��6�   6�   �� 5&   
   -   5        $ tablazatTab��5&      $
��Left=�6*   $	��447��6E   $��
253,24,0,0q�6R   $	��Top=�6f   $	��718��6~   .@�5'      �� $��Telephely lista��6   .��5(      � .+�5)      &   System.Windows.Controls.DataGrid  -   5          Name$ telephelyGrid��5)         
IsReadOnly .    AutoGenerateColumns .  $��	0,0,308,0q�6�   ��66   �� *Telephelyek+	 �� SelectedItem	 6Z   �� *KivTelephely+
   Columns
 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ Címe��61      Width5  /System.Windows.Controls.DataGridLengthConverter$	 180 6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *Cim+  5,      $ Dolgozók száma��61   $	 110 6I    6U   �� *Dolgozok.Count+  5-      $ Ketrecek száma��61   $	 110 6I    6U   �� *Ketrecek.Count+5/        50       .  .  $
��Left=�6�   $	��419��6�   $��	404,0,0,0q�6�   $	��Top=�6�   $	��308��6�   	 6M   �� *	KivKetrec+��6p   �� *
Ketrecek+
 51        52      $ ID��61   $ 50 6=    6H   �� *
KetrecID+  53      $ 	Állatfaj��61   $	 100 6C    6O   �� *Faj+  54      $ Méret��61   $ 75 6@    6K   �� *Meret+  55      $ Foglalt��61   $ 75 6B    6M   �� *Allatok.Count+57      59      5:      �� 5;      $��Gondozó lista��6   .��5<      � .+�5=        -   5        $ gondozoGrid��5=       .  .  ��64   �� *
Gondozok+	 6U   �� *
KivGondozo+
 5>        5?      $
 Név��61   $	 200 6>    6J   �� *Nev+  5@      $ 	Beosztás��61    6C   �� *
Beosztas+  5A      $ Utolsó Cselekvés��61    6K   �� *UtolsoCselekves+  5B      $ Gondozott állatai száma��61    6R   �� *GondozottAllatok.Count+  5C      $ Munkahelyek száma��61    6L   �� *Munkahelyek.Count+5E      5F      5G      5I      �� 5J   
   -   5        $ btfrissites��5J      $��	Frissites��6&   $
��Left=�6:   $��	873,0,0,0q�6U   $	��Top=�6~   $��75��6�   5K      5L      