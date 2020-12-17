// addressing: https://stackoverflow.com/questions/65307389/i-have-a-mutual-recursion-problem-in-f-sharp

open System

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

let rec find name ( familyTree_list : FamilyTree list) : FamilyTree =
    match familyTree_list with
    | (Person( try_name, _, _, _)::_) & (person::_) when name = try_name -> person 
    | (Person( _, _, _, children)::siblings) when (children <> [] || siblings <> []) ->
        let depth_search_result = find name children
        if Nothing <> depth_search_result then depth_search_result
        else find name siblings
    | _ -> Nothing

[<EntryPoint>]
let main argv =
    printfn "%A" (find "May" family_example)
    printfn "%A" (find "Joe" family_example)
    printfn "%A" (find "no one" family_example)
    0 // return an integer exit code
