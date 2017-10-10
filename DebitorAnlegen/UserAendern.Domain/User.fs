namespace UserAendern.Domain

type User = {
    Vorname : string
    Nachname : string
}



type ISaveUser =
    abstract CreateUser : user : User -> bool
    abstract UpdateUser : user : User -> bool

type IDeleteUser =
    abstract DeleteUser : user : User -> bool