VERSION 5.00
Begin VB.Form MainForm 
   Caption         =   "COMClientVBOptionalParam - Demo"
   ClientHeight    =   3090
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3090
   ScaleWidth      =   4680
   StartUpPosition =   3  '����ȱʡ
   Begin VB.TextBox txt_Result 
      Height          =   2895
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   0
      Top             =   120
      Width           =   4455
   End
End
Attribute VB_Name = "MainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Sub Form_Load()
    Call TestOptionalParam
End Sub

Private Sub TestOptionalParam()

    ' ����COM����
    Dim comObj As New MarshalOptionalParam.MarshalOptionalObj
        
    '���ÿ�ѡ��������, �ڶ�����Ĭ��ֵΪ10
    txt_Result.Text = "(�ڶ�����Ĭ��ֵΪ10)" + vbCrLf
    
    Dim result As Integer
    result = comObj.AddIntegerOptional(1)
    txt_Result.Text = txt_Result.Text + _
        "һ����������� " + CStr(result) + vbCrLf
    
    result = comObj.AddIntegerOptional(1, 2)
    txt_Result.Text = txt_Result.Text + _
        "������������� " + CStr(result) + vbCrLf
    
    ' �ͷ�COM����
    Set comObj = Nothing
End Sub


