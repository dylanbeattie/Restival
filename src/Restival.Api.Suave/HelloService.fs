module HelloService

open Suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Json
open Suave.Types
open Suave.Web
open Suave.Utils
open System.Net

open Restival.Api.Common.Resources

let config = { defaultConfig with 
                bindings = [HttpBinding.mk HTTP IPAddress.Loopback <| Sockets.Port.Parse "8080"] }
let serialize<'T> = UTF8.toString << toJson

let app =
  choose
    [ GET >>= Writers.setMimeType "application/json" >>= choose 
        [ path "/hello" >>= OK (serialize <| Greeting("World") )
          pathScan "/hello/%s" (fun name -> OK (serialize <| Greeting(name)))]
    ]

let start _ = startWebServer config app