export interface User {
  username: string;
  token: string;
}

/* About TypeScript

let data: number | string = 42;

data = "10"; // data = 10 also will work

interface Car {
  color: string;
  model: string;
  topSpeed?: number; // optional param
}

const car1 : Car = {
  color: 'red',
  model: 'Tesla'
}

// const cardError: {
//   color: 32, // any errors :(
//   model: 'wrong',
//   topSpeed: 100
// }

const multiply = (x: number, y: number): number => {
  return x * y;
}

*/
