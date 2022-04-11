VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "Form1"
   ClientHeight    =   4344
   ClientLeft      =   4296
   ClientTop       =   2448
   ClientWidth     =   4428
   LinkTopic       =   "Form1"
   ScaleHeight     =   4344
   ScaleWidth      =   4428
   Begin VB.TextBox Text1 
      Height          =   288
      Left            =   720
      TabIndex        =   0
      Text            =   "Text1"
      Top             =   1440
      Width           =   2892
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()

    Dim obj As New OV_SSO.clsOV_SSO
    Text1.Text = obj.GetLogon

End Sub
