   M S B A M L   `   `   `  ����  Admin_Client(    "Admin_Client.AllatAthelyezeseAblak   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$-  'Állat áthelyezése másik Telephelyre��5   	    � 
ResizeMode$ CanMinimize=�65    � WindowStartupLocation$ CenterScreen=�6N   $	��287��6s   $	��388��6�   .��5      � .+�5   
   �� $��Állat��6   $
��Left=�6"   $	��193��6=   $��	10,10,0,0q�6J   $	��Top=�6]   $	��362��6u   .��5      � .+�5      �� $��Állat neve: ��6   $
��Left=�6/   $��25��6J   $��5,10,0,0q�6V   $	��Top=�6h   $��93��6�   �� 5	      -   5         ��Name$ AllatNeveComboBox��5	       �� DisplayMemberPath$	 Nev��6�   $
��Left=�6�   $��25��6�   $��
104,10,0,0q�6�   $	��Top=�6�   $	��236��6�    �� SelectedItem 66   �� *
KivAllat+��6X   �� *JelKetrec.Allatok+6�   �� 5
      $��Jelenlegi telephely:��6   $
��Left=�67   $��27��6R   $��5,40,0,0q�6^   $	��Top=�6p   $	��160��6�   �� 5      $��Jelenlegi ketrec:��6   $
��Left=�64   $��27��6O   $��
180,40,0,0q�6[   $	��Top=�6o   $	��160��6�   �� 5      -   5         ��Name$ jelTelephelyLabel��5   ?   $
��Left=�6Z   $��27��6u   $��5,72,0,0q�6�   $	��Top=�6�   $	��160��6�   $��Center=�6�   ��6   �� *JelKetrec.Hely.Cim+6�   �� 5      -   5        $ jelKetrecLabel��5   ?   $
��Left=�6W   $��27��6r   $��
180,72,0,0q�6~   $	��Top=�6�   $	��160��6�   $��Center=�6�   ��6   �� *JelKetrec.KetrecID+6�   �� 5      $��Cél telephely:��6   $
��Left=�61   $��27��6L   $��	5,104,0,0q�6X   $	��Top=�6k   $	��160��6�    J� RenderTransformOrigin$ 	0.479,1.2&�6�   �� 5      $��Cél ketrec:��6   $
��Left=�6.   $��27��6I   $��180,104,0,0q�6U   $	��Top=�6j   $	��160��6�   $ 	0.479,1.2&�6�   �� 5      -   5        $ TelephelyeComboBox��5      $	 Cim��6�   $
��Left=�6�   $��25��6�   $��	5,136,0,0q�6�   $	��Top=�6�   $	��160��6�    67   �� *KivTelephely+��6]   �� *Telephelyek+6�   �� 5      -   5        $ KetreceComboBox��5      $ KetrecID��6x   $
��Left=�6�   $��25��6�   $��180,136,0,0q�6�   $	��Top=�6�   $	��160��6�   $ 
0.81,-0.88&�6�    64   �� *	KivKetrec+��6W   �� *
Ketrecek+6�   5      5      �� 5   
   -   5         ��Name$ MegseButton��5      $��Mégse��6&   $	��120��66   $��
35,216,0,0q�6B   $��25��6V   $	��Top=�6b   $
��Left=�6z   6�   �� 5   
   -   5        $ MentesButton��5      $��Mentés��6'   $	��120��68   $��
0,216,35,0q�6D   $��25��6X   $	��Top=�6d   $��Right=�6|   6�   5      5      