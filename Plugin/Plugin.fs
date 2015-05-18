namespace SSP.Plugin

open SSP.Core

type Plugin() =
    interface IPlugin with
        member x.Run(): int =    
            42