open System

[<EntryPoint>]
let main argv = 
    try
        printfn "starting to run"
        let plugins = PluginLoader.load
        printfn "%d plugins loaded" (plugins |> Seq.length)
        plugins |> Seq.iter (fun x -> printfn "%d" (x.Run ()))
    with
    | exn ->
        printfn "%s" exn.Message
        printfn "%s" exn.InnerException.Message
    
    printfn "done"
    Console.ReadKey() |> ignore

    0 // return an integer exit code
