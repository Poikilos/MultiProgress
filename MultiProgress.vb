''                                                                                      
''  Name:                                                                               
''                                                                                      
''      TextProgressBar                                                                 
''                                                                                      
''  Description:                                                                        
''                                                                                      
''      Text code for a custom progress bar control. This extension to the standard     
''      progress bar allows for a text overlay that can display either a percentage     
''      complete value or any other custom string.                                      
''                                                                                      
''  Audit:                                                                              
''                                                                                      
''      2019-04-18  rj  Original code                                                   
''      2021-07-31 Jake Gustafson  ForeColor, BackColor, fix missing text, invalidate to ensure draw, "%" instead of "" to show percent
''                                                                                      

'
'MIT License 
'Copyright(c) 2019 rj on Daniweb, 2021 Jake "Poikilos" Gustafson
'
'Permission Is hereby granted, free Of charge, to any person obtaining a copy
'of this software and associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'copies of the Software, and to permit persons to whom the Software Is
'furnished to do so, subject to the following conditions
'
'The above copyright notice and this permission notice shall be included In all
'copies Or substantial portions of the Software.
'
'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
'IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
'LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
'OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
'SOFTWARE.
'

' - This Control is based on the CustomProgress tutorial 2019-04-18 by RJ on
'   <https: //www.daniweb.com/programming/software-development/code/519610/vb-net-progress-bar-with-text>
' - An updated version may be found at:
'   https://github.com/poikilos/MultiProgress
'   - Please share code changes to this file to the URL above to ensure the continued development and quality (fork and submit a pull request)
'     and do not include any proprietary information in this file.

''' <summary>
''' This ProgressBar subclass can display custom Text or percentage progress if Text is "%".
''' Remember to enable Visual Styles if you want to see the ForeColor and BackColor.
''' </summary>
Public Class MultiProgress
    Inherits ProgressBar
    Private _Text As String = "" '''< The text to overlay onto the progress bar is private so that the Text Property can intercept when it is set and take action.
    Public OutlineBrush As Brush = Nothing
    Public BackBrush As Brush = Nothing
    Public ForeBrush As Brush = Nothing
    'Public BackPen As Pen = Nothing
    'Public ForePen As Pen = Nothing

    ''' <summary>
    ''' Text to overlay on top of the progress bar.
    ''' </summary>
    ''' <returns>Get the current Text.</returns>
    Public Overrides Property Text As String
        Get
            Return _Text
        End Get
        Set(value As String)
            If value Is Nothing Then
                value = ""
            End If
            If _Text.Equals(value) Then
                Exit Property
            End If
            _Text = value
            Me.Invalidate() 'Ensure the control draws again to remove the old text.
        End Set
    End Property
    'Public Property Text As String
    Private Const WmPaint = 15

    ''' <summary>
    ''' WndProc processes all messages for the control.
    ''' To prevent ProgressBar from painting over Text, run the custom code last.
    ''' </summary>
    ''' <param name="m"></param>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        If m.Msg = WmPaint Then
            Using g = Me.CreateGraphics()
                PaintBar(g)
                PaintText(g)
            End Using
        End If
    End Sub

    'Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
    ' MyBase.OnPaint(e)
    'PaintBar(e.Graphics)
    'PaintText(e.Graphics)
    'End Sub

    Private Sub PaintBar(g)
        OutlineBrush = New System.Drawing.SolidBrush(Color.Gray)
        'If IsNothing(BackBrush) Then
        BackBrush = New System.Drawing.SolidBrush(BackColor)
        'End If
        'If IsNothing(ForeBrush) Then
        ForeBrush = New System.Drawing.SolidBrush(ForeColor)
        'End If
        'Using g = Me.CreateGraphics()
        'Dim backPen As Pen = New Pen(BackBrush)
        'End Using
        'If ProgressBarRenderer.IsSupported Then
        '    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, ClientRectangle)
        'End If
        g.FillRectangle(BackBrush, ClientRectangle)
        Dim progressW As Integer = ClientRectangle.Width * Value / Maximum
        g.FillRectangle(ForeBrush, New Rectangle(New Point(0, 0), New Size(progressW, ClientRectangle.Height)))
        g.DrawRectangle(New Pen(OutlineBrush, 1), New Rectangle(New Point(0, 0), New Size(progressW, ClientRectangle.Height)))
    End Sub

    Private Sub PaintText(g As Graphics)
        ' To center the text, measure it first:
        ' Display Text or if "%" then change it to a calculated percentage.
        Dim displayStr As String = Text
        If displayStr = "%" Then
            Dim percent = CInt((Value - Minimum) * 100 / (Maximum - Minimum))
            displayStr = percent.ToString & "%"
        End If
        Dim textSize = g.MeasureString(displayStr, Me.Font)
        g.DrawString(displayStr, Me.Font, Brushes.Black, Me.Width / 2 - textSize.Width / 2, Me.Height / 2 - textSize.Height / 2)
    End Sub
End Class
