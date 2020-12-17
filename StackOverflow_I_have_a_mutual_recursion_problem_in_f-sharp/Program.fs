// addressing: https://stackoverflow.com/questions/65307389/i-have-a-mutual-recursion-problem-in-f-sharp

open System

// I have the following types in F#:

type Name = string;;
type Sex = 
    | M // male
    | F // female
type YearOfBirth = int;;
type FamilyTree = P of Name * Sex * YearOfBirth * Children
and Children = FamilyTree list;;

//here is an example:
let f1 = P("Larry",M,1920,[P("May",F,1945,[P("Fred",M,1970,[])]);P("Joe",M,1950,[P("Adam",M,1970,[])]);P("Paul",M,1955,[])])

(*My task is to create a function: find: Name -> FamilyTree -> returns (found name, sex, year, [List of the names of all their children]

I know it has something to do with mutual recursion but I am not sure how to apply it. This is what I wrote so far:*)

let fstn (f:FamilyTree) =
    match f with
    |P(n,s,y,c) -> n

let rec find n t = function
    |P(n1,s1,y1,cs) -> if n1=n then (n1,s1,y1,List.map (fun x -> fstn x) cs) 
                           else findC n cs
and findC n clist =
    match clist with
    |[] -> []
    |c::cs -> if n = fstn c then find n c 
                            else findC n cs

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
    printfn "Hello World from F#!"
    0 // return an integer exit code
