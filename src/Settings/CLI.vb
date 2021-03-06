﻿#Region "Microsoft.VisualBasic::c714fde1c648911e2b1f96e89b150da0, ..\Settings\CLI.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.

#End Region

Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData

<PackageNamespace("GCModeller.Configuration.CLI",
                  Category:=APICategories.CLI_MAN,
                  Url:="http://gcmodeller.org",
                  Description:="GCModeller configuration console.",
                  Publisher:="xie.guigang@gcmodeller.org",
                  Revision:=215)>
Public Module CLI

    Sub New()
        Settings.Session.Initialize()
    End Sub

    <ExportAPI("Set", Info:="Setting up the configuration data node.",
               Usage:="Set <varName> <value>",
               Example:="Set java /usr/lib/java/java.bin")>
    Public Function [Set](args As CommandLine) As Integer
        Using Settings = Global.GCModeller.Configuration.Settings.Session.ProfileData
            Dim params As String() = args.Parameters
            Dim x As String = params(0)
            Dim Value As String = params(1)

            Call Settings.Set(x, Value)
        End Using
        Return 0
    End Function

    <ExportAPI("var", Info:="Gets the settings value.",
               Usage:="var [varName]",
               Example:="")>
    <ParameterInfo("[VarName]", True,
                   Description:="If this value is null, then the program will prints all of the variables in the gcmodeller config file or when the variable is presents in the database, only the config value of the specific variable will be display.")>
    Public Function Var(args As CommandLine) As Integer
        Using Settings = Global.GCModeller.Configuration.Settings.Session.ProfileData
            If args.Parameters.Length = 0 Then 'list all setting items
                Call Console.WriteLine(Settings.View)
            Else
                Dim x As String = args.Parameters.First
                Call Console.WriteLine(Settings.View(x))
            End If
        End Using

        Return 0
    End Function
End Module

