module PluginLoader

open System
open System.IO
open System.Reflection
open SSP.Core


let directoryPath = AppDomain.CurrentDomain.BaseDirectory
let basePath = Directory.GetParent(directoryPath).Parent.Parent.Parent.FullName
let pluginPath = basePath + @"\Plugin\bin\Debug\"
let files () = Directory.GetFiles(pluginPath,"*.dll") |> Seq.toList

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

let load =
    let f = files ()
    List.collect getPluginsFromFile (f)
