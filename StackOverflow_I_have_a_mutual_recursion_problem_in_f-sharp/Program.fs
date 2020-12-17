// addressing: https://stackoverflow.com/questions/65307389/i-have-a-mutual-recursion-problem-in-f-sharp

open System

// I have the following types in F#:

type Name = string
type Sex = 
    | M // male
    | F // female
type YearOfBirth = int;;
type FamilyTree = 
    | Person of Name * Sex * YearOfBirth * Children
    | Nothing 
and 
    Children = FamilyTree list

//here is an example:
let family_example = [Person("Larry",M,1920,
                        [Person("May",F,1945,
                            [Person("Fred",M,1970,[])]);
                             Person("Joe",M,1950,
                                [Person("Adam",M,1970,[])]);
                             Person("Paul",M,1955,[])
                             ]
                         )]

(*My task is to create a function: find: Name -> FamilyTree -> returns (found name, sex, year, [List of the names of all their children]

I know it has something to do with mutual recursion but I am not sure how to apply it. This is what I wrote so far:*)

let firstName (f:FamilyTree) =
    match f with
    |Person( name, _, _, _) ->  name

let rec find name ( familyTree_list : FamilyTree list) : FamilyTree =
    match familyTree_list with
    | (person::_) when name = (firstName person) -> person 
    | (Person( _, _, _, children)::siblings) when (children <> [] || siblings <> []) ->
        let depth_search = find name children
        if Nothing <> depth_search then depth_search
        else find name siblings
    | _ -> Nothing

(*When I run

find "May" f1;;

I get:

error FS0001: This expression was expected to have type
    'Name * Sex * YearOfBirth * Name list'
but here has type
    ''a list'

Does anyone know what I am doing wrong? I know there is a problem with types but I am not sure how to fix it. I could use all the help I could get, thank you very much!*)


[<EntryPoint>]
let main argv =
    printfn "%A" (find "May" family_example)
    printfn "%A" (find "Joe" family_example)
    printfn "%A" (find "no one" family_example)
    0 // return an integer exit code
