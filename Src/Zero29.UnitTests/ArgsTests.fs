﻿namespace Ploeh.ZeroToNine.UnitTests

open Ploeh.ZeroToNine
open Ploeh.ZeroToNine.Versioning
open Xunit
open Xunit.Extensions

module ArgsTests =
    [<Theory>]
    [<InlineData("1.0.0.0")>]
    [<InlineData("1.1.0.0")>]
    [<InlineData("1.0.1.0")>]
    [<InlineData("1.0.0.1")>]
    [<InlineData("2.0.0.0")>]
    [<InlineData("2.2.0.0")>]
    [<InlineData("2.0.2.0")>]
    [<InlineData("2.0.0.2")>]
    [<InlineData("3.0.0.0")>]
    [<InlineData("3.3.0.0")>]
    [<InlineData("3.0.3.0")>]
    [<InlineData("3.0.0.3")>]
    [<InlineData("4.0.0.0")>]
    [<InlineData("4.4.0.0")>]
    [<InlineData("4.0.4.0")>]
    [<InlineData("4.0.0.4")>]
    [<InlineData("5.0.0.0")>]
    [<InlineData("5.5.0.0")>]
    [<InlineData("5.0.5.0")>]
    [<InlineData("5.0.0.5")>]
    [<InlineData("6.0.0.0")>]
    [<InlineData("6.6.0.0")>]
    [<InlineData("6.0.6.0")>]
    [<InlineData("6.0.0.6")>]
    [<InlineData("7.0.0.0")>]
    [<InlineData("7.7.0.0")>]
    [<InlineData("7.0.7.0")>]
    [<InlineData("7.0.0.7")>]
    [<InlineData("8.0.0.0")>]
    [<InlineData("8.8.0.0")>]
    [<InlineData("8.0.8.0")>]
    [<InlineData("8.0.0.8")>]
    [<InlineData("9.0.0.0")>]
    [<InlineData("9.9.0.0")>]
    [<InlineData("9.0.9.0")>]
    [<InlineData("9.0.0.9")>]
    let ParseAssignVersionReturnsCorrectResult(version : string) =
        let actual = [| "-a"; version |] |> Args.Parse
        Assert.Equal<Arg>([Assign(version)], actual |> Seq.toList)

    [<Theory>]
    [<InlineData("major",    Rank.Major)>]
    [<InlineData("minor",    Rank.Minor)>]
    [<InlineData("build",    Rank.Build)>]
    [<InlineData("patch",    Rank.Build)>]
    [<InlineData("revision", Rank.Revision)>]
    let ParseIncementReturnsCorrectResult(rank : string, expectedRank : Rank) =
        let actual = [| "-i"; rank |] |> Args.Parse
        Assert.Equal<Arg>([Increment(expectedRank)], actual |> Seq.toList)

    [<Fact>]
    let ParseListVersion() =
        let actual = [| "-l" |] |> Args.Parse
        Assert.Equal<Arg>([ListVersions], actual |> Seq.toList)

    [<Theory>]
    [<InlineData("-?")>]
    [<InlineData("-h")>]
    let ParseHelpArgsReturnsShowHelp(switch : string) =
        let actual = [| switch |] |> Args.Parse
        Assert.Equal<Arg>([ShowHelp], actual |> Seq.toList)

    [<Fact>]
    let ParseEmptyArgsReturnsShowHelp() =
        let actual = [| |] |> Args.Parse
        Assert.Equal<Arg>([ShowHelp], actual |> Seq.toList)

    [<Theory>]
    [<InlineData("-a")>]
    [<InlineData("-c ")>]
    [<InlineData("-c b")>]
    [<InlineData("-h b")>]
    [<InlineData("-s major")>]
    [<InlineData("-s minor")>]
    [<InlineData("-s build")>]
    [<InlineData("-s patch")>]
    [<InlineData("-s revision")>]
    [<InlineData("-i")>]
    let ParseUnknownArgsReturnsCorrectResult(argString : string) =
        let args = argString.Split() |> Array.toList
        let actual = args |> Args.Parse
        Assert.Equal<Arg>([Unknown(args)], actual |> Seq.toList)
