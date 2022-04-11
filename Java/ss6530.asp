<%
	Dim vt
	Dim writeIt
	writeIt = true
	
	If Not IsEmpty(Session("terminal")) Then
		Set vt = Session("terminal")
	Else
		Session.Timeout = 5
		Set vt = Server.CreateObject("Vt6530.Vt6530")
		Set Session("terminal") = vt
	End If

	If Trim(UCase(Request.ServerVariables("REQUEST_METHOD"))) = "GET" Then
		vt.setPort 1016
		vt.setHost "is"
		vt.connect
		vt.waitConnect
		vt.fakeKeys "prod"
		vt.fakeKey 10, false, false, false
		If vt.waitENQ() = false Then
			Response.Write "<HTML><HEAD>Request Timed Out</HEAD><BODY></BODY></HTML>"
			writeIt = false
		End If
	Else
		dim key
		dim x
		key = Request("screen")("hdnKey")
		For x = 0 to Request("screen").count -1
			vt.setField x, Request("screen")("F" & x)
		Next
		Select Case key
			Case "F1":
				vt.fakeF1
				Break
			Case "Enter":
				vt.fakeEnter
				Break
		End Select
		vt.waitENQ
	End If
	
	Response.Write vt.toHTML()

%>