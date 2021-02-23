Funcionalidade: Simulador

Com um Analista de Qualidade, 
Eu gostaria de simular um investimento na poupança através de uma chamada REST
Para que como resposta da requisição possa receber as projeções de acordo com o Mês e Valor.

Contexto:

Dado Eu defini as configurações REST

@automated
@positive
@done
Esquema do Cenário: Simulação de poupança com permutações válidas
	Quando Eu realizo a operação POST para simular um investimento na poupança:
	|Perfil   | ValorAplicado  | PoupadoPorMes   | PeriodoPoupado  | TipoPeriodo  |
    |<perfil> | <valorAplicado>| <poupadoPorMes> | <periodoPoupado>| <tipoPeriodo>|
	Então será retornado as projeções de rentabilidade <tempo> (Meses) X R$ <valores>
	E o status retornado deverá ser 200

Exemplos: 
|perfil      |valorAplicado|poupadoPorMes|periodoPoupado|tipoPeriodo |tempo                        | valores                                |
|paraVoce    |20,0         |20,0         |1             |M           |1, 13, 25, 37, 49            | 40, 282, 528, 777, 1.029               | 
|paraVoce    |20,0         |20,0         |1             |A           |12, 24, 36, 48, 60           | 262, 507, 756, 1.008, 1.263            |
|paraVoce    |20,0         |20,0         |999           |M           |999, 1011, 1023, 1035, 1047  | 37.704, 38.473, 39.253, 40.044, 40.846 |
|paraVoce    |20,0         |20,0         |99            |A           |1188, 1200, 1212, 1224, 1236 | 51.157, 52.114, 53.086, 54.070, 55.069 |
|paraEmpresa |20,0         |20,0         |1             |M           |1, 13, 25, 37, 49            | 40, 282, 528, 777, 1.029               | 
|paraEmpresa |20,0         |20,0         |1             |A           |12, 24, 36, 48, 60           | 262, 507, 756, 1.008, 1.263            |
|paraEmpresa |20,0         |20,0         |999           |M           |999, 1011, 1023, 1035, 1047  | 37.704, 38.473, 39.253, 40.044, 40.846 |
|paraEmpresa |20,0         |20,0         |99            |A           |1188, 1200, 1212, 1224, 1236 | 51.157, 52.114, 53.086, 54.070, 55.069 |

@automated
@negative
@done
Esquema do Cenário: Simulação de poupança com permutações inválidas
	Quando Eu realizo a operação POST para simular um investimento na poupança:
	|Perfil   | ValorAplicado  | PoupadoPorMes   | PeriodoPoupado  | TipoPeriodo  |
    |<perfil> | <valorAplicado>| <poupadoPorMes> | <periodoPoupado>| <tipoPeriodo>|
	Então deverá ser retornado Erro dados
	E o status retornado deverá ser 200
Exemplos: 
|perfil      |valorAplicado |poupadoPorMes|periodoPoupado|tipoPeriodo |
|Pessoal     |19,99         |19,99        |-1            |M           |
|Pessoal     |9999999,99    |9999999,99   |-1            |M           |
|Pessoal     |              |             |              |M           |
|paraEmpresa |19,99         |19,99        |-1            |M           |
|paraEmpresa |9999999,99    |9999999,99   |-1            |M           |
|paraEmpresa |              |             |              |M           |
|Pessoal     |19,99         |19,99        |-1            |A           |
|Pessoal     |9999999,99    |9999999,99   |-1            |A           |
|Pessoal     |              |             |              |A           |
|paraEmpresa |19,99         |19,99        |-1            |A           |
|paraEmpresa |9999999,99    |9999999,99   |-1            |A           |
|paraEmpresa |              |             |              |A           |
