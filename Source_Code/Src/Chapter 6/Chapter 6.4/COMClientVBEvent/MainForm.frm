VERSION 5.00
Begin VB.Form MainForm 
   Caption         =   "COMClientVBEvent - Demo"
   ClientHeight    =   2565
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4740
   LinkTopic       =   "Form1"
   ScaleHeight     =   2565
   ScaleWidth      =   4740
   StartUpPosition =   3  '����ȱʡ
   Begin VB.TextBox txt_Result 
      Height          =   2175
      Left            =   240
      MultiLine       =   -1  'True
      TabIndex        =   0
      Top             =   240
      Width           =   4335
   End
End
Attribute VB_Name = "MainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public WithEvents comObj As MarshalEvent.StockPriceControlObj
Attribute comObj.VB_VarHelpID = -1

'�¼�������
Private Sub comObj_OnPriceChanged( _
        ByVal newPrice As Double, ByVal oldPrice As Double)
     txt_Result.Text = txt_Result.Text + "�¹ɼۣ� " + _
        CStr(newPrice) + "  ǰһ�����չɼۣ� " + _
        CStr(oldPrice) + vbCrLf
End Sub

Private Sub Form_Load()
    '����COM����ʵ��
    Set comObj = New MarshalEvent.StockPriceControlObj
    
    '�����¼�
    comObj.ChangeStockPrice (18)
    comObj.ChangeStockPrice (38)
    comObj.ChangeStockPrice (28)
    comObj.ChangeStockPrice (58)
    
    
    '�ͷ�COM����
    Set comObj = Nothing
End Sub


