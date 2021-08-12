let data : number | string;

data = '42';

data = 10;

interface ICar{
    color:string;
    model:string;
    topSpeed?:number;
}

const cart1:ICar={
    color:'blue',
    model:'bmw'
};


const cart2:ICar={
    color:'red',
    model:'mercedes',
    topSpeed:100
};

const multiply=(x:number,y:number):number=>{
    return x * y;
};