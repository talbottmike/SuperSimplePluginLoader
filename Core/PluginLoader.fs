module PluginLoader

open System.IO
open System.Reflection
open SSP.Core

let files path = Directory.GetFiles(path,"*.Plugin*.dll") |> Seq.toList

let getPluginsFromFile file =
        let a = Assembly.LoadFrom(file)
        [ for t in a.GetTypes() do
            for i in t.GetInterfaces() do
                if i.Name = "IPlugin" && i.Namespace = "SSP.Core" then
                    let pluginObject = 
                        a.CreateInstance(t.FullName)
                        :?> IPlugin
                    yield pluginObject ]
//                    match pluginObject with 
//                    | :? IPlugin -> 
//                        printfn "Valid plugin loaded %s" t.FullName
//                        yield a.CreateInstance(t.FullName) :?> IPlugin
//                    | _ -> printfn "Invalid plugin %s" t.FullName ]

let loadFromPath path =
    let f = files path
    List.collect getPluginsFromFile (f)
