   M S B A M L   `   `   `  ���� E  APhotoCraft, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PhotoCraft.ViewsPhotoCraft.Views  =#clr-namespace:PhotoCraft.ViewModelsPhotoCraft.ViewModels  "    PhotoCraft.Views.CropperView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:PhotoCraft.Views     2vm#clr-namespace:PhotoCraft.ViewModels     .���   � RowDefinitions  �� $
��auto ��� $��* �.+��� $��0q�$��Browse��$��Center=�$��	10,10,0,0q�   
ForeGround#���   $��24(�$
��Left=�$	��Top=�$��50��$	��150��$
��Bold"�  ElementColor#���  ���� *BrowseCommand+ � 	Resources  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 30���� #���   $��24(�$��
180,15,0,0q�$	��800��$��40��$
��Left=�$	��Top=����� *DisplayedImagePath+�� $��0q�$
��Save��$��Center=�$��	10,80,0,0q�#���   $��24(�$
��Left=�$	��Top=�$��50��$	��150��$
��Bold"�#���  ���� *SaveLocationCommand+ ��%:   '��       �� " ��.���  �@��CornerRadius$ 30���� #���   $��24(�$��
180,85,0,0q�$	��800��$��40��$
��Left=�$	��Top=����� *SaveLocation+�� $��0q�$��Start��$��Center=�$��10,10q�  	TextColor#���  $��24(�$��Right=�$	��Top=�$��50��$	��150��$
��Bold"�  
SnackColor#���  ���� *StartProcessCommand+{��� *	IsEnabled+ ��%:   '��       �� " ��.���  �@��CornerRadius$ 30���� $��0q�$��Center=�$��	0,80,10,0q�#���  $��24(�$��Right=�$	��Top=�$��50��$	��150��$
��Bold"�#���  ���� *	SafePaths+ �� SelectedIndex �� *SelectedIndex+ �� SelectedItem �� *SelectedBoxItem+� $��1��$��20q�$
��Left=�$	��Top=�$	��900��$	��500�� � ColumnDefinitions �� �� .+��� $��0��#���   �� CornerRadius$ 20��$��10q�._�� 
������ .+��� -   	 ��Name$	 
PictureBox������ *DisplayedImage+�� -   $��inkCanvasMeasure��
 �� 
Background

 ����  �� EditingMode$
 None=�$��Center=� �� Strokes �� *
InkStrokes+ �� Mode$ TwoWay=�����  �� ElementName$ 
PictureBox�� �� Path$ ActualWidth����� $ 
PictureBox��$ ActualHeight� �� DefaultDrawingAttributes �� *drawingAttributes+�� $��1��#���  $ 20��$��10q�._��� -   $	 
PreviewBox������ *PreviewedImage+