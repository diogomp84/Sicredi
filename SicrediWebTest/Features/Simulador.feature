Funcionalidade: Simulador

Com um Associado, 
Eu gostaria de simular um investimento na poupança
Para que possa preencher o formulário de simulação e ver a tabela de resultado com Mês e Valor.

Contexto: 
	Dado a página do simulador de investimento foi carregada com sucesso

@automated
@positive
@done
Esquema do Cenário: Simulação de poupança com permutações válidas
	Quando Eu realizo a simulação:
	|Perfil   | ValorAplicado  | PoupadoPorMes   | PeriodoPoupado  | TipoPeriodo  |
    |<perfil> | <valorAplicado>| <poupadoPorMes> | <periodoPoupado>| <tipoPeriodo>|
	Então vejo que em <qtMensal> meses teria guardado <valorTotal> 
	E vejo outras opções de rentabilidade projetada <tempo> (Meses) X R$ <valores>

Exemplos: 
|perfil      |valorAplicado|poupadoPorMes|periodoPoupado|tipoPeriodo | valorTotal |qtMensal |tempo                  | valores                        |
|Pessoal     |20.00        |20.00        |1             |Meses       | R$ 40      |1        |13, 25, 37, 49         | 282, 528, 777, 1.029           | 
|Pessoal     |20.00        |20.00        |1             |Anos        | R$ 262     |12       |24, 36, 48, 60         | 507, 756, 1.008, 1.263         |
|Pessoal     |20.00        |20.00        |999           |Meses       | R$ 37.704  |999      |1011, 1023, 1035, 1047 | 38.473, 39.253, 40.044, 40.846 |
|Pessoal     |20.00        |20.00        |99            |Anos        | R$ 51.157  |1188     |1200, 1212, 1224, 1236 | 52.114, 53.086, 54.070, 55.069 |

|Empresarial |20.00        |20.00        |1             |Meses       | R$ 40      |1        |13, 25, 37, 49         | 282, 528, 777, 1.029           |
|Empresarial |20.00        |20.00        |1             |Anos        | R$ 262     |12       |24, 36, 48, 60         | 507, 756, 1.008, 1.263         |
|Empresarial |20.00        |20.00        |999           |Meses       | R$ 37.704  |999      |1011, 1023, 1035, 1047 | 38.473, 39.253, 40.044, 40.846 |
|Empresarial |20.00        |20.00        |99            |Anos        | R$ 51.157  |1188     |1200, 1212, 1224, 1236 | 52.114, 53.086, 54.070, 55.069 |

@automated
@negative
@done
Esquema do Cenário: Simulação de poupança com permutações inválidas
	Quando Eu realizo a simulação:
	|Perfil   | ValorAplicado  | PoupadoPorMes   | PeriodoPoupado  | TipoPeriodo  |
    |<perfil> | <valorAplicado>| <poupadoPorMes> | <periodoPoupado>| <tipoPeriodo>|
	Então vejo que o formulário de simulação apresenta mensagem correspondente ao campo:
	| Key           | Value              |
	| ValorAplicar  | <msgValorAplicar>  |
	| ValorInvestir | <msgValorInvestir> |
	| Tempo         | <msgTempo>         |

Exemplos: 
|perfil      |valorAplicado |poupadoPorMes|periodoPoupado|tipoPeriodo | msgValorAplicar       | msgValorInvestir      | msgTempo                   |
|Pessoal     |19.99         |19.99        |-1            |Meses       | Valor mínimo de 20.00 | Valor mínimo de 20.00 | Valor esperado não confere |
|Pessoal     |9.999.999,99  |9.999.999,99 |-1            |Meses       | Máximo de 9999999.00  | Máximo de 9999999.00  | Valor esperado não confere |
|Pessoal     |              |             |              |Meses       | Valor mínimo de 20.00 | Valor mínimo de 20.00 | Obrigatório                |
|Empresarial |19.99         |19.99        |-1            |Meses       | Valor mínimo de 20.00 | Valor mínimo de 20.00 | Valor esperado não confere |
|Empresarial |9.999.999,99  |9.999.999,99 |-1            |Meses       | Máximo de 9999999.00  | Máximo de 9999999.00  | Valor esperado não confere |
|Empresarial |              |             |              |Meses       | Valor mínimo de 20.00 | Valor mínimo de 20.00 | Obrigatório                |

@manual
@planned
Cenário: Refazer uma simulação de poupança
	Dado Eu já tenha realizado uma simução
	Quando Eu realizo a simulação:
	|Perfil   | ValorAplicado  | PoupadoPorMes | PeriodoPoupado  | TipoPeriodo  |
    |Pessoal  | 20.00          | 20.00         | 12              | Meses        |
	Então vejo que em 12 meses teria guardado R$ 262