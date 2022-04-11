VERSION 5.00
Object = "{EDA1E561-6F0F-11D4-98C6-000102494781}#1.0#0"; "Vt6530_Terminal_Proj.dll"
Begin VB.Form frmMain 
   Caption         =   "VT6530"
   ClientHeight    =   6405
   ClientLeft      =   2205
   ClientTop       =   1665
   ClientWidth     =   8955
   LinkTopic       =   "Form1"
   ScaleHeight     =   427
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   597
   Begin VT6530_TERMINAL_PROJLibCtl.Vt6530Control Vt6530Control1 
      Height          =   5295
      Left            =   240
      OleObjectBlob   =   "frmMain.frx":0000
      TabIndex        =   0
      Top             =   120
      Width           =   8415
   End
   Begin VB.Frame Frame1 
      Height          =   735
      Left            =   240
      TabIndex        =   1
      Top             =   5640
      Width           =   8415
      Begin VB.CommandButton cmdOpen 
         Caption         =   "Open Date"
         Height          =   375
         Left            =   5880
         TabIndex        =   8
         Top             =   240
         Width           =   1215
      End
      Begin VB.CommandButton cmdAcct 
         Caption         =   "601601601"
         Height          =   375
         Left            =   4560
         TabIndex        =   7
         Top             =   240
         Width           =   1215
      End
      Begin VB.CommandButton cmd101 
         Caption         =   "101"
         Height          =   375
         Left            =   3480
         TabIndex        =   6
         Top             =   240
         Width           =   975
      End
      Begin VB.CommandButton cmdProd 
         Caption         =   "Prod"
         Height          =   375
         Left            =   1200
         TabIndex        =   5
         Top             =   240
         Width           =   975
      End
      Begin VB.CommandButton cmdLogon 
         Caption         =   "Logon"
         Height          =   375
         Left            =   2280
         TabIndex        =   4
         Top             =   240
         Width           =   1095
      End
      Begin VB.CommandButton cmdDisconnect 
         Caption         =   "Disconnect"
         Height          =   375
         Left            =   7200
         TabIndex        =   3
         Top             =   240
         Width           =   1095
      End
      Begin VB.CommandButton cmdConnect 
         Cancel          =   -1  'True
         Caption         =   "Connect"
         Height          =   375
         Left            =   120
         TabIndex        =   2
         Top             =   240
         Width           =   975
      End
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'*************************************************************************
'   W A S H I N G T O N    S T A T E    D E P T    O F   R E V E N U E
'
'                   Vt6530 ActiveX control demonstation
'
'*************************************************************************

' flag for disconnecting on exit
Private blnConnected As Boolean

' Control left position for resizing
Private intLeft As Long

' Control top position for resizing
Private intTop As Long

'
'  cmd101_Click
'
'   Navigate from the system menu to the 101 screen.  The
'   user must already be on the system menu for this to
'   work.
'
Private Sub cmd101_Click()
    Vt6530Control1.FakeKeys "100"
    Vt6530Control1.FakeF1
    Vt6530Control1.waitENQ
    Vt6530Control1.FakeKeys "101"
    Vt6530Control1.FakeF1
End Sub

'
'   cmdAcct_Click
'
'   Enter "601601601" and press F1.  The user must
'   be on a screen where this makes sense (like 101).
'
Private Sub cmdAcct_Click()
    Vt6530Control1.FakeKeys "601601601"
    Vt6530Control1.FakeF1
    Vt6530Control1.waitENQ
End Sub

'
'   cmdConnect_Click
'
'   Connect to the Tandem
'
Private Sub cmdConnect_Click()
    If Not blnConnected Then
        Vt6530Control1.Connect 20
    End If
End Sub

'
'   cmdDisconnect_Click
'
'   Disconnect from the Tandem
'
Private Sub cmdDisconnect_Click()
    Vt6530Control1.Disconnect
End Sub

'
'   cmdLogon_Click
'
'   Enter user id, password, and press F1
'
Private Sub cmdLogon_Click()
    Vt6530Control1.FakeKeys "jrgts140eod4eod"
    Vt6530Control1.FakeF1
End Sub

'
'   cmdOpen_Click
'
'   Scrape the account open date from the 101 screen.
'
Private Sub cmdOpen_Click()
    MsgBox "Open Date is " & Vt6530Control1.Field(7)
End Sub

'
'   cmdProd_Click
'
'   Enter "prod" and hit return.  This only makes sense
'   on the available services menu.
'
Private Sub cmdProd_Click()
    Vt6530Control1.FakeKeys "prod"
    Vt6530Control1.FakeKey 13, 0, 0, 0
End Sub

Private Sub Form_Load()
    '
    '  Save the control position for resizing
    '
    intLeft = Vt6530Control1.Left
    intTop = Vt6530Control1.Top
End Sub

Private Sub Form_Resize()
    '
    '  Resize the control and position the buttons.
    '
    Frame1.Left = intLeft
    Frame1.Top = Me.ScaleHeight - Frame1.Height
    Frame1.Width = Me.ScaleWidth - intLeft
    
    Vt6530Control1.Height = (Me.ScaleHeight - Frame1.Height) - intTop * 2 - 11
    Vt6530Control1.Width = (Me.ScaleWidth - intLeft * 2) - 4
End Sub

Private Sub Form_Unload(Cancel As Integer)
    If blnConnected Then
        Vt6530Control1.Disconnect
    End If
End Sub

'
'   Control events.  Currently, calling the control in any
'   of these methods can cause problems.  Consider them
'   notifications only.
'

Private Sub Vt6530Control1_Connected()
    frmMain.Caption = "Connected"
    blnConnected = True
End Sub

Private Sub Vt6530Control1_Debug(ByVal sMessage As String)
    frmMain.Caption = "Debug: " & sMessage
End Sub

Private Sub Vt6530Control1_Disconnected()
    blnConnected = False
    frmMain.Caption = "Disconnected"
End Sub

Private Sub Vt6530Control1_Enquire()
    frmMain.Caption = "Enquire"
End Sub

Private Sub Vt6530Control1_Error(ByVal sMessage As String)
    frmMain.Caption = "Error: " & sMessage
End Sub
