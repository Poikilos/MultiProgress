# MultiProgress
This is a ProgressBar that has a Text overlay and uses ForeColor and BackColor even if Visual Styles are enabled.


## License
See the license file. Software from the second 2021-07-31 commit and on is considered original work by Jake Gustafson since any existing code was from a tutorial and all code providing core functionality had to be changed to make it work.


## Differences from original functionality by rj on Daniweb
- Show the `Text` (The problem may have been that the bar height was not high enough: See `Size` in the "Initialize" section).
- Implement `ForeColor` and `BackColor` even when Visual Styles are enabled in the application (override drawing completely--Override OnPaint didn't work, so keep using `WmPaint`).
- Only automatically set text to a percentage if the Text is set to "%" (instead of "")
- See also: [changelog.md](changelog.md)


## Use

### Declare
```vb.net
    Friend WithEvents ThisMultiProgress As MultiProgress

    'Create optional variables to store the initial colors (recommended):
    Public Property DefaultProgressBackColor As Color
    Public Property DefaultProgressForeColor As Color

```

### Initialize
```vb.net
        Me.ThisMultiProgress = New MultiProgress()

        '
        'ThisMultiProgress
        '
        Me.ThisMultiProgress.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ThisMultiProgress.Location = New System.Drawing.Point(3, 3)
        Me.ThisMultiProgress.Name = "ThisMultiProgress"
        Me.ThisMultiProgress.Size = New System.Drawing.Size(240, 24)
        Me.ThisMultiProgress.TabIndex = 7
        Me.ThisMultiProgress.ForeColor = Color.Green
        ' ^ The default is blue even though Visual Styles makes the regular ProgressBar always green.
        '   Use green to match the usual visual style such as for Windows 7 and Windows 10.

        'Store the initial colors (recommended):
        Me.DefaultProgressBackColor = Me.ThisMultiProgress.BackColor
        Me.DefaultProgressForeColor = Me.ThisMultiProgress.ForeColor
```

### Change color
```vb.net
        Me.ThisMultiProgress.BackColor = Color.White
        Me.ThisMultiProgress.ForeColor = Color.Red
        Me.ThisMultiProgress.Invalidate() 'Ensure that it draws with the new colors.
```

### Change the color back to normal
(This is possible if you added the optional code above)
```vb.net
        Me.ThisMultiProgress.BackColor = Me.DefaultProgressBackColor
        Me.ThisMultiProgress.ForeColor = Me.DefaultProgressForeColor
        Me.ThisMultiProgress.Invalidate() 'Ensure that it draws with the new color.
```

### Change Text
```vb.net
        Me.ThisMultiProgress.Text = ""
```

### Show a Percentage Value
The percentage shown will be generated (based on `Value` and `Maximum`).
```vb.net
        Me.ThisMultiProgress.Text = "%"
```
