# Card Game (PINS)
_Pins Is Not Slay the spire_

## Turnos
O **GameController** gerencia os turnos:
1. Player Modifiers.StartTurn
2. Player começar o turno (joga as cartas) e finaliza quando ele quiser.
3. Player Modifiers.EndTurn
4. Enemies Modifiers.StartTurn
5. Enemies fazem as ações deles em ordem
6. Enemies Modifiers.EndTurn

Eu temos que modificar algumas coisas:

1. Hoje o player não é uma entidade... Ele é um controller
Já q ele não existe em si no game
Temos que trocar para ele ser uma entidade como os outros inimigos.

2. Temos que criar a movimentação das entidades.

3. Alterar as cartas para funcionarem no Grid system

## Sistema de cartas

Quem faz o controle de turno das cartas (sacar/jogar/descartar) é o **PlayerController**.  
Todas as cartas são **Card** (ScriptableObject).  
Ela possui algumas coisas referentes a carta, como nome, custo, icone, tipo, e o mais importante: Efeitos.  
Os efeitos são as coisas que as cartas fazem.  
Cada efeito é um **BaseCardEffect** (ScriptableObject) diferente. Hoje, as cartas só fazem ações no *OnPlay*, mas é bem tranquilo de adicionar ações em coisas coisas (OnDraw, OnDiscard).  
Os efeitos são, por exemplo: Dar dano. Dar armor. Adicionar modifier. Dar dano em área, etc.    

A descrição da carta é formada pela concatenação das descrições dos efeitos juntos.  

## Entidades
Entidades contemplam Inimigos e o Player.

### Vida
Todas as entidades que tem vida (todas?) extendem uma classe da **IHealth**.

### Modifiers
Hoje, podem ser adicionados nerfs e buffs usando os Modifiers, para uma unidade ter modifiers, um de seus componentes deve extender do **IModifiersHolder**

## Dano
O dano é calculado pelo **DamageCalculator**. Ele leva em conta o dano (número), modifiers do damager, vida do target e modifiers do target.
Caso coloquemos algo a mais (equipes?), mudamos somente essa classe.
 
## Encontro
Quem gerencia os inimigos do encontro é o **EnemiesController**.  
Hoje, ele recebe um parâmetro de **Encounter** (ScriptableObject). Essa classe guarda os dados do encontro. Hoje, somente possui o Nome do encontro (interno) e os inimigos do encontro.  
**@WIP** Quando todos os inimigos são mortos, o **EnemiesController** comunica o **GameController** do final da batalha.
