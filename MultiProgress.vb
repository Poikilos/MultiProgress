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
''                                                                                      

Public Class Form1

    Private prgCopy As New CustomProgress

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Controls.Add(prgCopy)
        prgCopy.Dock = DockStyle.Bottom
        prgCopy.Value = 0.0
        prgCopy.Minimum = 0.0
        prgCopy.Maximum = 100.0
        prgCopy.Style = ProgressBarStyle.Continuous

    End Sub

    Private Sub btnShowText_Click(sender As Object, e As EventArgs) Handles btnShowText.Click

        prgCopy.Text = "display any custom text"

        For i As Integer = 1 To 100
            System.Threading.Thread.Sleep(25)
            prgCopy.Value = i
            prgCopy.Update()
            My.Application.DoEvents()
        Next

    End Sub

    Private Sub btnShowPercent_Click(sender As Object, e As EventArgs) Handles btnShowPercent.Click

        prgCopy.Text = ""

        For i As Integer = 1 To 100
            System.Threading.Thread.Sleep(25)
            prgCopy.Value = i
            prgCopy.Update()
            My.Application.DoEvents()
        Next

    End Sub

End Class

''                                                                                      
''  Name:                                                                               
''                                                                                      
''      CustomProgress                                                                  
''                                                                                      
''  Description:                                                                        
''                                                                                      
''      ProgressBar that allows display of custom text or percentage progress.          
''                                                                                      
''  Custom Properties:                                                                  
''                                                                                      
''      Text:     String    If Null then % progress is calculated and displayed, else   
''                the value of the string is displayed.                                 
''                                                                                      
''  Audit:                                                                              
''                                                                                      
''      2019-04-18  rj  Original Code                                                   
''                                                                                      

Public Class CustomProgress

    Inherits ProgressBar

    Public Overrides Property Text As String

    Private Const WmPaint = 15

    'WndProc receives all messages directed to the current window. In order to
    'avoid overwriting your text, make sure the default code executes first,
    'then call the custom routine to display the overlid text.

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        If m.Msg = WmPaint Then PaintText()
    End Sub

    Private Sub PaintText()

        Dim s As String = Text

        'Display either a percentage or custom text

        If s = "" Then
            Dim percent = CInt((Value - Minimum) * 100 / (Maximum - Minimum))
            s = percent.ToString & "%"
        End If

        'Get the graphics object and calculate drawing parameters based on the current Font specs

        Using g = Me.CreateGraphics()
            Dim textSize = g.MeasureString(s, Me.Font)
            Using b = New SolidBrush(ForeColor)
                g.DrawString(s, Me.Font, Brushes.Black, Me.Width / 2 - textSize.Width / 2, Me.Height / 2 - textSize.Height / 2)
            End Using
        End Using

    End Sub

End Class
