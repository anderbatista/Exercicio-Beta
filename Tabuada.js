let valor = prompt("----------------------------------------------------\n Qual tabuada do 1 ao 10 você quer consultar? \n -----------------------------------------------------");
let resultado;
let valorInteiro = valor % 1 === 0;

multiplicacao();

function multiplicacao(){
  
  if (valorInteiro && valor >= 1 && valor <= 10){   
    for (contador = 1; contador <= 10; contador++){
    resultado = valor * contador;
    console.log(valor + " x " + contador + " = " + resultado);
    
    }
}    
  else{
      console.log("Numero inválido");
    }
}   