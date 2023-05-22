// info-pessoais
const nameInput = document.getElementById('nome');
const naturalidade = document.getElementById('naturalidade');
const idade = document.getElementById('idade');
const estadoCivil = document.getElementById('estadoCivil');
const cidade = document.getElementById('cidade');
const estado = document.getElementById('estado');
const tel = document.getElementById('tel');
const telRecado = document.getElementById('telRecado');
const email = document.getElementById('email');
// info-objetivo
const objetivo = document.getElementById('objetivo');
// info-academica
const instituicao = document.getElementById('instituicao');
const diploma = document.getElementById('diploma');
const nomeCurso = document.getElementById('nomeCurso');
const dInicio = document.getElementById('d-inicio');
const dTermino = document.getElementById('d-termino');
// info-cursoComplementar
const descricaoCursoComplementar = document.getElementById('descricaoCursoComplementar');
// info-trabalho
const empresa = document.getElementById('empresa');
const cargo = document.getElementById('cargo');
const tInicio = document.getElementById('t-inicio');
const tTermino = document.getElementById('t-termino');
const descricaoJob = document.getElementById('descricaoJob');

const btnJob = document.getElementById('btn-job');
const btnSend = document.getElementById('btn-send');
const result = document.getElementById('result');

const form = document.getElementsByClassName('form-on')[0];

const createDiv = () => {
  const div = document.createElement('div');
  div.id = 'result-div'
  result.appendChild(div);
};

const paragrafo = (text) => {
  const div = document.getElementById('result-div');
  const p = document.createElement('p');
  p.className = 'text';
  p.innerHTML = text;
  div.appendChild(p);
};

const cabecalho = (text) => {
  const div = document.getElementById('result-div');
  const h = document.createElement('h1');
  h.className = 'text';
  h.innerHTML = text;
  div.appendChild(h);
};

const negrito = (text) => {
  const div = document.getElementById('result-div');
  const b = document.createElement('b');
  b.className = 'text';
  b.innerHTML = text;
  div.appendChild(b);
};

function chamaForms(e) {
  const p = document.getElementsByClassName('text');
  e.preventDefault();
  if (form.classList.contains('form-on')) {
    form.classList.add('form-off');
  }
  createDiv();
}

createButtonVoltar = () => {
  const resultDiv = document.getElementById('result-div');
  const result = document.getElementById('result-div');
  const btnVoltar = document.createElement('button');
  btnVoltar.id = 'btn-voltar';
  btnVoltar.innerHTML = 'Voltar';
  resultDiv.appendChild(btnVoltar);
};

voltarFunction = () => {
  const result = document.getElementById('result');
  const resultDiv = document.getElementById('result-div');
  if (form.classList.contains('form-off')) {
    form.classList.remove('form-off');
    result.removeChild(resultDiv);
  }
};

// função submit
btnSend.addEventListener('click', (e) => {
  if (nameInput.value.length <= 0) {
    chamaForms(e);
    cabecalho('Impossível fazer um curriculo sem o nome completo');
  }
  else {
    chamaForms(e);

    cabecalho(`${nameInput.value}`);
    paragrafo(`${naturalidade.value}, ${idade.value} anos, ${estadoCivil.value}, ${cidade.value} / ${estado.value}`);
    paragrafo(`Telefone: ${tel.value}, Recado: ${telRecado.value}`);
    paragrafo(`E-mail: ${email.value}`);

    cabecalho('Objetivo');
    paragrafo(objetivo.value);

    cabecalho(`Formação acadêmica`);
    negrito(instituicao.value);
    paragrafo(`${diploma.value}, ${nomeCurso.value} - ${dInicio.value} a ${dTermino.value}`);

    cabecalho(`Cursos complementares`);
    paragrafo(descricaoCursoComplementar.value);

    cabecalho(`Histórico profissional`);
    negrito(empresa.value);
    paragrafo(`Período: ${tInicio.value} a ${tTermino.value}`);
    paragrafo(`Cargo: ${cargo.value}`);
    paragrafo(`Descrição: ${descricaoJob.value}`);
  }
  createButtonVoltar();
  const btnVoltar = document.getElementById('btn-voltar');
  btnVoltar.addEventListener('click', () => {
    voltarFunction();
  });
});
