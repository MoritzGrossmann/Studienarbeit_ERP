﻿namespace UserAendern.Domain

open System.Security.Cryptography.X509Certificates

type User = {
    FirstName : string
    LastName : string
    FullName : string
    UserName : string
}

type Address = {
    Street : string
    Number : string
    Postcode : string
    City : string
}

type Lock = {
    WrongLogOn : bool
    NoPassword : bool
    LocalLock : bool
    GlobalLock : bool
}

type UserDetails = {
    Firstname: string
    Lastname : string
    Address : Address
    IsLocked : Lock
}

type UserCreate = {
    User: User
    Address : Address
}

type SaveResponse = {
    Erfolg : bool
    Message : string
}

type BapiReturnType 
    = Success
    | Error
    | Warning
    | Info
    | Abort

type BapiReturn = {
    Type : BapiReturnType
    Message : string
}



type SapBool
    = X
    | Nothing
    | Other

type ISaveUser = 
    abstract CreateUser : user : User   -> password : string ->BapiReturn
    abstract DeleteUser : user : string -> BapiReturn
    abstract ChangeUser : user : User   -> userDetails : UserDetails -> BapiReturn
    abstract LockUser   : user : string -> BapiReturn
    abstract UnlockUser : user : string -> BapiReturn
    

type ILoadUser = 
    abstract GetUsers : User seq
    abstract GetUserAddress : username : string -> Address
    abstract GetUserDetail  : username : string -> UserDetails