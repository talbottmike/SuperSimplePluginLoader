open System
open System.IO

[<EntryPoint>]
let main argv = 
    try
        printfn "starting to run"

        let directoryPath = AppDomain.CurrentDomain.BaseDirectory
        let basePath = Directory.GetParent(directoryPath).Parent.Parent.Parent.FullName
        let fSharpPluginPath = basePath + @"\Plugin\bin\Debug\"
        let cSharpPluginPath = basePath + @"\CSharpPlugin\bin\Debug\"

        let executePlugins path = 
            let plugins = PluginLoader.loadFromPath path
            let pluginCount = plugins |> Seq.length
            printfn "%d plugins loaded from path %s" pluginCount path
            plugins |> Seq.iter (fun x -> printfn "%d" (x.Run ()))

        // Try with C#
        // Commenting out the following line causes the fSharp plugins to fail to cast. Leaving it in causes them to cast successfully. How is that possible?
        //executePlugins cSharpPluginPath

        // Try with F#
        executePlugins fSharpPluginPath
    with
    | exn ->
        printfn "%s" exn.Message
        match exn.InnerException with
        | null -> ()
        | x -> printfn "%s" x.Message
    
    printfn "done"
    Console.ReadKey() |> ignore

    0 // return an integer exit code
