Imports System.Math
Imports System.IO

Module Module1

    Sub Main()

        Dim polarAngle As Decimal
        Dim polarRadius As Decimal
        Dim cartesianX As Decimal
        Dim cartesianY As Decimal
        Dim xyzFile As System.IO.StreamWriter
        Dim commandlineFileName As String = Environment.CommandLine
        Dim polarFileName As String = commandlineFileName.Remove(0, 12)
        Dim xyzFileName As String = polarFileName.Remove((polarFileName.Length - 4), 4) & ".xyz"

        Using MyReader As New Microsoft.VisualBasic.
                      FileIO.TextFieldParser(polarFileName)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",", ":")

            xyzFile = My.Computer.FileSystem.OpenTextFileWriter(xyzFileName, True)

            Dim currentField(3) As String
            While Not MyReader.EndOfData
                Try
                    currentField = MyReader.ReadFields()

                    polarAngle = Val(currentField(1))
                    polarRadius = Val(currentField(3))

                    cartesianX = polarRadius * Math.Cos(polarAngle * (Math.PI / 180))
                    cartesianY = polarRadius * Math.Sin(polarAngle * (Math.PI / 180))

                    If polarRadius > 0 Then
                        xyzFile.WriteLine((20000 + cartesianX) & ", " & (20000 + cartesianY) & ", 0.000")
                    End If

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try

            End While
        End Using
        xyzFile.Close()
    End Sub

End Module
