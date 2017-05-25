Attribute VB_Name = "DnnContentApiDemo"
Public Const const_ContentApiURL = "https://qa.dnnapi.com/content"
Public const_ApiKey As String

' Return with response string
Function apiContentTypeGetRequest() As String
    Dim MyRequest As MSXML2.XMLHTTP
    Dim MyResponse As String
    Dim FullApiURL As String
    
    Set MyRequest = CreateObject("MSXML2.XMLHTTP.6.0")
    FullApiURL = const_ContentApiURL & "/api/ContentTypes?searchText=&fieldOrder=createdAt&orderAsc=false&startIndex=0&maxItems=5000"
    With MyRequest
        .Open "GET", FullApiURL
        .setRequestHeader "authorization", "Bearer " & const_ApiKey
        .send ""
        MyResponse = MyRequest.responseText
    End With
    'Debug.Print MyResponse
    apiContentTypeGetRequest = MyResponse
End Function

' Return with response string
Function apiContentItemGetRequest(ByVal contentTypeId As String) As String
    Dim MyRequest As MSXML2.XMLHTTP
    Dim MyResponse As String
    Dim FullApiURL As String
    
    Set MyRequest = CreateObject("MSXML2.XMLHTTP.6.0")
    FullApiURL = const_ContentApiURL & "/api/ContentItems/?startIndex=0&maxItems=5000&fieldOrder=createdAt&orderAsc=false&searchText=&contentTypeId=" & contentTypeId
    With MyRequest
        .Open "GET", FullApiURL
        .setRequestHeader "authorization", "Bearer " & const_ApiKey
        .send ""
    
        MyResponse = MyRequest.responseText
    End With
    'Debug.Print MyResponse
    apiContentItemGetRequest = MyResponse
End Function

Function createRecord2Remote(ws As Worksheet, aRow As Range) As String
    
    Dim selected As String
    Dim contentTypeId As String
    Dim responseStr, requestStr As String
    Dim JsonObject, myItem As New Dictionary
    Dim items As New Collection
    Dim Parsed As Object
    Dim RcdCount As Long
    
    Set JsonObject = New Dictionary
    JsonObject.Add "details", New Dictionary
    JsonObject("details").Add "jobTitle", aRow.Cells(4).Value
    JsonObject("details").Add "description", aRow.Cells(5).Value
    JsonObject("details").Add "department", New Collection
    JsonObject.Add "name", aRow.Cells(4).Value
    JsonObject.Add "description", ""
    JsonObject.Add "tags", New Collection
    JsonObject.Add "contentTypeId", aRow.Cells(7).Value
    requestStr = JsonConverter.ConvertToJson(JsonObject)
    Debug.Print requestStr
    responseStr = apiContentItemPostRequest(requestStr)
    
    ' If Post Successful, Update the list in worksheet.
    If responseStr <> "" Then
        Set Parsed = ParseJson(responseStr)
        ' Update the row data with responsed data
        Call updateRowData(ws, responseStr, aRow, True)
        
    End If


End Function

Function UpdateRecord2Remote(ws As Worksheet, aRow As Range) As String
    
    Dim selected As String
    Dim contentTypeId As String
    Dim responseStr, requestStr As String
    Dim JsonObject, myItem As New Dictionary
    Dim items As New Collection
    Dim Parsed As Object
    Dim RcdCount As Long
    
    Set JsonObject = New Dictionary
    JsonObject.Add "details", New Dictionary
    JsonObject("details").Add "jobTitle", aRow.Cells(4).Value
    JsonObject("details").Add "description", aRow.Cells(5).Value
    JsonObject("details").Add "department", New Collection
    JsonObject.Add "name", aRow.Cells(4).Value
    JsonObject.Add "description", ""
    JsonObject.Add "tags", New Collection
    JsonObject.Add "contentTypeId", aRow.Cells(7).Value
    requestStr = JsonConverter.ConvertToJson(JsonObject)
    Debug.Print requestStr
    responseStr = apiContentItemPutRequest(requestStr, aRow.Cells(1))
    
    ' If Post Successful, Update the list in worksheet.
    If responseStr <> "" Then
        Set Parsed = ParseJson(responseStr)
        ' Update the row data with responsed data
        Call updateRowData(ws, responseStr, aRow, False)
        
    End If
    'ws.Range("B10").Value = JsonConverter.ConvertToJson(JsonObject)

End Function

Sub updateRowData(ByVal ws As Worksheet, ByVal responseStr As String, ByVal aRow As Range, ByVal isNew As Boolean)
    Dim Parsed As Object
    Dim Field, ColCount As Long
    
    ColCount = 8
    Set Parsed = ParseJson(responseStr)
    
    ws.Cells(aRow.Row, 1).Value = Parsed("id")
    ws.Cells(aRow.Row, 2).Value = Parsed("slug")
    ws.Cells(aRow.Row, 3).Value = Parsed("Tags")
    ws.Cells(aRow.Row, 4).Value = Parsed("details")("jobTitle")
    ws.Cells(aRow.Row, 5).Value = Parsed("details")("description")
    ws.Cells(aRow.Row, 6).Value = printStringObj(Parsed("details")("department"))
    ws.Cells(aRow.Row, 7).Value = Parsed("contentTypeId")
    ws.Cells(aRow.Row, 8).Value = Parsed("contentTypeName")
    
    If isNew Then
        ws.CheckBoxes.Add(Cells(aRow.Row, 9).Left + 5, Cells(aRow.Row, 9).Top + 2, 17, 17).Select
        With Selection
            .Caption = ""
            .Value = xlOff
            .LinkedCell = Col_Letter(9) & "$" & aRow.Row
            .Display3DShading = False
        End With
        
        ws.Range("A1:C" & CStr(aRow.Row)).Locked = True

        ws.Range("A1:C" & CStr(aRow.Row)).Select
        With Selection.Interior
            .Pattern = xlSolid
            .PatternColorIndex = xlAutomatic
            .ThemeColor = xlThemeColorDark1
            .TintAndShade = -0.14996795556505
            .PatternTintAndShade = 0
    End With
        
    Else
        Call ProcessCheckBox(ws)
        
    End If
   
    
    
End Sub

' Return with response string
Function apiContentItemPostRequest(ByVal jsonString As String) As String
    Dim MyRequest As MSXML2.XMLHTTP
    Dim MyResponse As String
    Dim FullApiURL As String
    
    Set MyRequest = CreateObject("MSXML2.XMLHTTP.6.0")
    FullApiURL = const_ContentApiURL & "/api/ContentItems/?publish=true"
    With MyRequest
        .Open "POST", FullApiURL
        .setRequestHeader "content-type", "application/json"
        .setRequestHeader "authorization", "Bearer " & const_ApiKey
        .send jsonString
    End With

    MyResponse = MyRequest.responseText
    If MyRequest.Status <> 201 Then
        MsgBox ("Content Item Post Request Failed with Error Code: " & MyRequest.statusText & vbCrLf & "Dismiss this dialog to continue.")
        MyResponse = ""
    End If

    'Debug.Print MyResponse
    apiContentItemPostRequest = MyResponse
End Function

' Return with response string
Function apiContentItemPutRequest(ByVal jsonString As String, ByVal contentItemId As String) As String
    Dim MyRequest As MSXML2.XMLHTTP
    Dim MyResponse As String
    Dim FullApiURL As String
    Debug.Print jsonString
    Set MyRequest = CreateObject("MSXML2.XMLHTTP.6.0")
    FullApiURL = const_ContentApiURL & "/api/ContentItems/" & contentItemId & "/?publish=true"
    With MyRequest
        .Open "PUT", FullApiURL
        .setRequestHeader "content-type", "application/json"
        .setRequestHeader "authorization", "Bearer " & const_ApiKey
        .send jsonString
        '{"details":{"jobTitle":"Job Post1","description":"<p style=\"margin-left: 20px;\">Job Post Description 1\n</p>","department":[]},"name":"Job Post1","description":"","tags":[],"contentTypeId":"5f066a5c-6d10-4048-a45b-cc515e300ec6"}
        
        MyResponse = MyRequest.responseText
        If MyRequest.Status <> 200 Then
            MsgBox ("Content Item Post Request Failed with Error Code: " & MyRequest.Status & "." & vbCrLf & "Dismiss this dialog to continue.")
            MyResponse = ""
        End If
    End With
    'Debug.Print MyResponse
    apiContentItemPutRequest = MyResponse
End Function


Sub refreshContentTypeListBox(ByVal responseStr As String)
    Dim Parsed As Object
    Dim RcdCount As Long
    Dim MyListBox As Object
    Dim ws As Worksheet

    Set Parsed = ParseJson(responseStr)
    RcdCount = Parsed("totalResultCount")
    Debug.Print RcdCount
    Set ws = Worksheets("ControlPage")
    ws.Shapes.Range(Array("List Box 1")).Select
    With Selection
        .RemoveAllItems
        .MultiSelect = xlNone
        For i = 1 To RcdCount
            '.AddItem (Parsed("documents")(i)("name") & fillSpaces(Parsed("documents")(i)("name"), 25) & ">>" & Parsed("documents")(i)("id"))
            .AddItem ("<" & Parsed("documents")(i)("id") & ">" & fillSpaces(Parsed("documents")(i)("id"), 40) & Parsed("documents")(i)("name"))
        Next i
        .selected = 1
    End With
End Sub

Sub refreshContentItemList(ByVal responseStr As String)
    Dim Parsed, test As Object
    Dim Row, RcdCount, ColCount As Long
    Dim MyListBox As Object
    Dim ws As Worksheet
    Dim ContentItemKeys As Variant
    Dim fieldKeyName, keyArray(1 To 100) As String
    
    
    Set Parsed = ParseJson(responseStr)
    RcdCount = Parsed("totalResultCount")
    Debug.Print RcdCount
    ' Add a new worksheet
    Set ws = Worksheets.Add()
    ' Prepare for the Field Title row
    ws.Cells(1, 1).Value = "id"
    ws.Cells(1, 2).Value = "slug"
    ws.Cells(1, 3).Value = "Tags"
    ColCount = 4
    'Parsed.Keys
    'Parsed("documents")(1).Keys
    'test = Parsed("documents")(1).Items
    For Each ContentItemKeys In Parsed("documents")(1)("details").Keys
        If ContentItemKeys <> 1 Then
            ws.Cells(1, ColCount).Value = ContentItemKeys
            keyArray(ColCount) = ContentItemKeys
            Debug.Print ContentItemKeys
            ColCount = ColCount + 1
        End If
    Next ContentItemKeys
    ws.Cells(1, ColCount).Value = "contentTypeId"
    ws.Cells(1, ColCount + 1).Value = "contentTypeName"
    ws.Cells(1, ColCount + 2).Value = "Selected"
    ws.Range(Cells(1, 1), Cells(1, ColCount + 2)).Select
    With Selection.Interior
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
        .ThemeColor = xlThemeColorDark1
        .TintAndShade = -0.149998474074526
        .PatternTintAndShade = 0
    End With
    ws.Columns("A:Z").Select
    Selection.ColumnWidth = 8
    ws.Columns("A:A").Select
    Selection.ColumnWidth = 28
    ws.Columns("E:E").Select
    Selection.ColumnWidth = 35
    ws.Columns("G:G").Select
    Selection.ColumnWidth = 28
    ws.Columns("H:H").Select
    Selection.ColumnWidth = 12
    Cells.Select
    With Selection
        .RowHeight = 20
        .Font.Size = 8
        .HorizontalAlignment = xlGeneral
        .VerticalAlignment = xlTop
        .WrapText = True
    End With
    ' Fill up Content Items
    RcdCount = Parsed("totalResultCount")
    For Row = 1 To RcdCount
        fieldKeyName = ws.Cells(1, 1).Value
        ws.Cells(Row + 1, 1).Value = Parsed("documents")(Row)("id")
        ws.Cells(Row + 1, 2).Value = Parsed("documents")(Row)("slug")
        ws.Cells(Row + 1, 3).Value = Parsed("documents")(Row)("Tags")
        
        For Field = 4 To ColCount
            ws.Cells(Row + 1, Field).Value = printStringObj(Parsed("documents")(Row)("details")(keyArray(Field)))
        Next Field
        
        ws.Cells(Row + 1, ColCount).Value = Parsed("documents")(Row)("contentTypeId")
        ws.Cells(Row + 1, ColCount + 1).Value = Parsed("documents")(Row)("contentTypeName")
        
        ws.CheckBoxes.Add(Cells(Row + 1, ColCount + 2).Left + 5, Cells(Row + 1, ColCount + 2).Top + 2, 17, 17).Select
        With Selection
            .Caption = ""
            .Value = xlOff
            .LinkedCell = Col_Letter(ColCount + 2) & "$" & Row + 1
            .Display3DShading = False
        End With
        
    Next Row
    
    
    ws.Range("A1:C" & CStr(RcdCount + 1)).Locked = True
    
    ws.Range("A1:C" & CStr(RcdCount + 1)).Select
    With Selection.Interior
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
        .ThemeColor = xlThemeColorDark1
        .TintAndShade = -0.14996795556505
        .PatternTintAndShade = 0
    End With
    
    Call createButtonSync2Remote(ws)
End Sub

Function printStringObj(ByVal anyInput As Variant) As String
    If VarType(anyInput) = 9 Then
        printStringObj = ""
    Else
        printStringObj = anyInput
    End If
End Function


Function fillSpaces(ByVal originStr As String, ByVal maxLength As Long) As String
    Dim i As Long
    For i = 1 To maxLength - Len(originStr)
        fillSpaces = fillSpaces & " "
    Next i
End Function

Sub scanWorkSheet(ByVal wsName As String)
    Dim ws As Worksheet
    Dim rowCount, changedRowCount, newCreatedCount, chkBoxField As Long
    Dim aRow As Range
    Dim responseStr As String
    
    const_ApiKey = Worksheets("ControlPage").Shapes("TextBox1").OLEFormat.Object.Object.Value
    Set ws = Worksheets(wsName)
    rowCount = 0
    chkBoxField = ws.Range("1:1").Find("Selected").Column
    
    newCreatedCount = 0
    changedRowCount = 0
    For Each aRow In ws.Rows
        If aRow.Row > 1 Then ' 1st Row is table titles
            If ws.Cells(aRow.Row, 1).Value = "" And ws.Cells(aRow.Row, 2).Value = "" And ws.Cells(aRow.Row, 3).Value = "" And ws.Cells(aRow.Row, 4).Value = "" And ws.Cells(aRow.Row, 5).Value = "" Then
                Exit For
            End If
            rowCount = rowCount + 1
            'Debug.Print ws.Cells(aRow.Row, chkBoxField).Value
            If ws.Cells(aRow.Row, 1).Value = "" Then  ' ID is empty means create a new one.
                responseStr = createRecord2Remote(ws, aRow)
                newCreatedCount = newCreatedCount + 1
            ElseIf ws.Cells(aRow.Row, chkBoxField).Value Then ' Checked and Update required.
                responseStr = UpdateRecord2Remote(ws, aRow)
                changedRowCount = changedRowCount + 1
            End If

        End If
    Next aRow
    Debug.Print rowCount
    MsgBox "Totally " & newCreatedCount & " new Content Items created, and " & changedRowCount & " Items updated."
    
    
End Sub


Function Col_Letter(lngCol As Long) As String
    Dim vArr
    vArr = Split(Cells(1, lngCol).Address(True, False), "$")
    Col_Letter = vArr(0)
End Function

Sub ProcessCheckBox(ws As Worksheet)
    Dim cb As Shape
    
    'Loop through Checkboxes
      For Each cb In ws.Shapes
        If cb.Type = msoFormControl Then
          If cb.FormControlType = xlCheckBox Then
            If cb.ControlFormat.Value = 1 Then
              'Do something if checked...
              cb.ControlFormat.Value = -4146
            ElseIf cb.ControlFormat.Value = -4146 Then
              'Do something if not checked...
            ElseIf cb.ControlFormat.Value = 2 Then
              'Do something if mixed...
            End If
          End If
        End If
      Next cb
  
End Sub


Sub createButtonSync2Remote(ws As Worksheet)
    Dim t As Range
    Set t = ws.Cells(11, 10)
    Set btn = ws.Buttons.Add(t.Left, t.Top, 150, 24)
    With btn
      .OnAction = "'scanWorkSheet " & """" & ws.Name & """" & "'"
      .Caption = "Update To Remote >>>"
      .Name = "Btn_Sync2Remote"
    End With
  
End Sub
