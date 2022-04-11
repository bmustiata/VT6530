VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "Get Open Date"
   ClientHeight    =   3030
   ClientLeft      =   4185
   ClientTop       =   2685
   ClientWidth     =   4665
   LinkTopic       =   "Form1"
   ScaleHeight     =   3030
   ScaleWidth      =   4665
   Begin VB.CommandButton cmdLookup 
      Caption         =   "Lookup"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   14.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1680
      TabIndex        =   4
      Top             =   2160
      Width           =   1455
   End
   Begin VB.TextBox txtTRA 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   1440
      TabIndex        =   0
      Text            =   "601601601"
      Top             =   600
      Width           =   2295
   End
   Begin VB.Label lblDate 
      Caption         =   "N/A"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2160
      TabIndex        =   3
      Top             =   1440
      Width           =   1695
   End
   Begin VB.Label Label2 
      Alignment       =   1  'Right Justify
      Caption         =   "Open Date:"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   2
      Top             =   1440
      Width           =   1695
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      Caption         =   "TRA"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   600
      Width           =   1095
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'
'  cmdLookup_Click
'
'  Lookup TRA end date on Tandem screen 101
'
Private Sub cmdLookup_Click()
    On Error GoTo aErr
    
    Screen.MousePointer = vbHourglass
    
    Dim vt As Vt6530
    Set vt = CreateObject("Vt6530.Vt6530")
    
    '
    '  Connect to Tandem
    '
    vt.Connect 20
    vt.wait34
    
    '
    '  Enter PROD logon screen
    '
    vt.FakeKeys "prod"
    vt.FakeKey 13, 0, 0, 0
    vt.waitENQ
    
    '
    ' Logon
    '
    vt.FakeKeys "jrgts140"
    vt.FakeKeys "eod4eod"
    vt.FakeF1
    vt.waitENQ
    
    '
    '  Goto the business registration screen
    '
    vt.FakeKeys "100"
    vt.FakeF1
    vt.waitENQ
    
    '
    ' Goto the TRA inquiry screen
    '
    vt.FakeKeys "101"
    vt.FakeF1
    vt.waitENQ
    
    '
    '  Enter the account number
    '
    vt.FakeKeys txtTRA.Text
    vt.FakeF1
    vt.waitENQ
    
    '
    '  Get the data from field 7.  The Field collection
    '  counts protected and non protected fields.
    '
    lblDate.Caption = vt.Field(7)
    'MsgBox vt.getScreenDump
    
    '
    '  Drop connection
    '
    vt.Disconnect
bye:
    Screen.MousePointer = vbDefault
    Exit Sub
aErr:
    MsgBox Err.Description & vbCrLf & vbCrLf & vt.getScreenDump
    Resume bye
End Sub

