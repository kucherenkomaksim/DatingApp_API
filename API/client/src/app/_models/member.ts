import { Photo } from "./photo";

export interface Member {
  userName: string
  gender: string
  age: number
  nickName: string
  created: Date
  lastActive: Date
  introduction: string
  lookingFor: string
  interests: string
  city: string
  country: string,
  photoUrl: string,
  photos: Photo[]
}
