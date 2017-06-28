namespace Talamoana.Functional

open FSharp.Data

// type Foo = JsonProvider<"""{ "next_change_id": "123121-321312-312312", "stashes": [] }""">
// let simple = Foo.Parse(""" { "next_change_id": "abvde", "stashes": [] } """)


module Say =
    let hello name =
        printfn "Hello %s" name
