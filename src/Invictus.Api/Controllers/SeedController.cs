
using Invictus.Application.AuthApplication;
using Invictus.Application.AuthApplication.Interface;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Invictus.Domain.Administrativo.ContratosAggregate;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.Parametros;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/seed")]
    public class SeedController : BaseController
    {
        private InvictusDbContext _db;
        private readonly IAdmApplication _admApp;
        private string Unidade {get;set;}
        public SeedController(InvictusDbContext db, IAdmApplication admApp)
        {
            _db = db;
            _admApp = admApp;
        }

        [HttpGet]
        [Route("create")]
        public async Task<ActionResult> CreateInitialData()
        {

            var unidadeCG = new Unidade("CGI", "Invictus Campo Grande", "Campo Grande",
              "23088000", "", "Estrada da Posse", "3700", "Rio de Janeiro", "RJ", true);
            var salaUmUnidadeCG = new Sala("Sala Auditório", "Com ar-condicionado", 70, true);
            var salaDoisUnidadeCG = new Sala("Sala Frente", "Sem ar-condicionado", 40, true);
            unidadeCG.Salas.Add(salaUmUnidadeCG);
            unidadeCG.Salas.Add(salaDoisUnidadeCG);

            var unidadeSJ = new Unidade("SJM", "Invictus São João de Meriti", "Vilar dos teles",
              "25555800", "", "Rua Geni", "40 - T", "São João de Meriti", "RJ", true);
            var salaUmUnidadeSJ = new Sala("Sala Auditório", "Com ar-condicionado", 50, true);
            var salaDoisUnidadeSJ = new Sala("Sala Frente", "Sem ar-condicionado", 30, true);
            unidadeSJ.Salas.Add(salaUmUnidadeSJ);
            unidadeSJ.Salas.Add(salaDoisUnidadeSJ);

            await _db.Unidades.AddAsync(unidadeCG);
            await _db.Unidades.AddAsync(unidadeSJ);
            await _db.SaveChangesAsync();

            var cargos = new List<Cargo>();

            await _db.Cargos.AddRangeAsync(
                new Cargo(null, "Atendente", true),
                new Cargo(null, "Aux. Administrativo", true),
                new Cargo(null, "Coordenador", true),
                new Cargo(null, "Diretor", true),
                new Cargo(null, "Gerente", true),
                new Cargo(null, "Pedagogia", true),
                new Cargo(null, "Secretaria Escolar", true),
                new Cargo(null, "Sócio Investidor", true),
                new Cargo(null, "Sócio Diretor", true),
                new Cargo(null, "Técnico", true),
                new Cargo(null, "Telemarketing", true),
                new Cargo(null, "Vendedor", true),
                new Cargo(null, "Professor", true)
                );

            await _db.SaveChangesAsync();

            var paramType = new ParametrosType("Feriados");

            var paramValues1 = new ParametrosValue("01/01");
            var paramValues2 = new ParametrosValue("15/02");
            var paramValues3 = new ParametrosValue("16/02");
            var paramValues4 = new ParametrosValue("17/02");
            var paramValues5 = new ParametrosValue("01/04");
            var paramValues6 = new ParametrosValue("02/04");
            var paramValues7 = new ParametrosValue("21/04");
            var paramValues8 = new ParametrosValue("01/05");
            var paramValues9 = new ParametrosValue("03/06");
            var paramValues10 = new ParametrosValue("07/11");
            var paramValues11 = new ParametrosValue("12/10");
            var paramValues12 = new ParametrosValue("02/11");
            var paramValues13 = new ParametrosValue("15/11");
            var paramValues14 = new ParametrosValue("24/12");
            var paramValues15 = new ParametrosValue("25/12");
            var paramValues16 = new ParametrosValue("31/12");

            paramType.ParametrosValues.Add(paramValues1);
            paramType.ParametrosValues.Add(paramValues2);
            paramType.ParametrosValues.Add(paramValues3);
            paramType.ParametrosValues.Add(paramValues4);
            paramType.ParametrosValues.Add(paramValues5);
            paramType.ParametrosValues.Add(paramValues6);
            paramType.ParametrosValues.Add(paramValues7);
            paramType.ParametrosValues.Add(paramValues8);
            paramType.ParametrosValues.Add(paramValues9);
            paramType.ParametrosValues.Add(paramValues10);
            paramType.ParametrosValues.Add(paramValues11);
            paramType.ParametrosValues.Add(paramValues12);
            paramType.ParametrosValues.Add(paramValues13);
            paramType.ParametrosValues.Add(paramValues14);
            paramType.ParametrosValues.Add(paramValues15);
            paramType.ParametrosValues.Add(paramValues16);

            await _db.ParametrosTypes.AddAsync(paramType);
            await _db.SaveChangesAsync();

            var typePacote1 = new TypePacote("APH/ENFERMAGEM", null, "Avançado", true);
            var typePacote2 = new TypePacote("AUXILIAR DE FARMÁCIA", null, "Básico", true);
            var typePacote3 = new TypePacote("AUXILIAR DE NECROPSIA E TANATOPRAXIA", null, "Intermediário", true);
            var typePacote4 = new TypePacote("CBMERJ", null, "Intensivo", true);
            var typePacote5 = new TypePacote("CUIDADOR", null, "Intermediário", true);
            var typePacote6 = new TypePacote("FORMAÇÃO DE SOCORRISTA", null, "Intermediário", true);

            await _db.TypePacotes.AddAsync(typePacote1);
            await _db.TypePacotes.AddAsync(typePacote2);
            await _db.TypePacotes.AddAsync(typePacote3);
            await _db.TypePacotes.AddAsync(typePacote4);
            await _db.TypePacotes.AddAsync(typePacote5);
            await _db.TypePacotes.AddAsync(typePacote6);

            await _db.SaveChangesAsync();


            var newContrato = new ContratoDto();
            newContrato.ativo = true;
            newContrato.titulo = "Contrato APH/ENFERMAGEM";
            newContrato.conteudo = @"<div style=""text - align: justify; ""><span style=""background - color: transparent; font - size: 1rem; "">“CONDIÇÕES GERAIS DE CONTRATAÇÃO” 1. O ALUNO e a CONTRATADA, ambos qualificados no TERMO DE ADESÃO - QUADRO DE CONTRATAÇÃO resolve celebrar o presente Contrato de Prestação de Serviços Educacionais (o “CONTRATO”), no qual estão expressas as condições segundo as quais a CONTRATADA ministrará o curso escolhido pelo ALUNO no QUADRO DE CONTRATAÇÃO, mediante o pagamento, pelo ALUNO, dos valores constantes do referido QUADRO. 2. Aplicam-se também ao presente CONTRATO, como parte integrante e indissociável do mesmo, o Regimento Escolar do Sistema de Ensino Invictus, bem como o Manual do Aluno dos Cursos Técnicos e/ou de Qualificação Profissional, ambos disponíveis para consulta na secretaria do curso, documentos estes que o ALUNO declara ter lido previamente à assinatura deste CONTRATO, obrigando-se, ainda a cumprir com o quanto ali está disposto. As Partes comprometem-se, ainda, a cumprir com a legislação educacional e a pautar suas ações com base na boa-fé e equilíbrio contratual. 3.O aluno deverá frequentar as aulas e o campo de estagio supervisionado devidamente uniformizado conforme regimento interno da instituição. 4. O ALUNO que realizar o Curso Técnico na modalidade concomitante ao Ensino Médio, fica ciente que a possível Diplomação no Curso Técnico, somente ocorrerá após a apresentação do Certificado de Conclusão do Ensino Médio, com a respectiva publicação em Diário Oficial (D.O.) e assim como com o laudo do processo de autenticidade quando solicitado pelo inspetor escolar da secretaria de educação do Estado do Rio Janeiro, quando for o caso. 5. A formulação e divulgação das diretrizes e orientações técnicas relativas à prestação dos serviços de ensino são de inteira responsabilidade da CONTRATADA, especialmente no que tange à avaliação do rendimento escolar do ALUNO, à fixação de carga horária e grade curricular de qualquer um de seus cursos, à indicação de professores e atividades curriculares, à adoção de determinada modalidade de ensino e à orientação didático-pedagógica do ALUNO, razão pela qual, a CONTRATADA poderá, a qualquer tempo, realizar alterações nas atividades aqui mencionadas, alterações estas que serão previamente informados ao ALUNO pelos canais de comunicação disponíveis para tanto (e-mail, carta, telemarketing, etc). 5.1. A CONTRATADA também se reserva o direito de (i) incluir disciplinas ministradas à distância nos projetos pedagógicos dos cursos presenciais, até o limite de 40 % (quarenta por cento) da grade curricular; (ii) reduzir ou ampliar a duração do curso, a seu critério exclusivo, tendo em vista que o prazo de duração do mesmo é totalmente estimada; (iii) introduzir aulas em outros dia da semana ou finais de semana , de forma alternada ou seguida. 5.2. A CONTRATADA poderá propor alteração no calendário escolar e na organização das turmas, agrupamento de classes, alterações de horário ou outras medidas que se fizerem necessárias para o melhor aproveitamento do curso, sempre respeitando a carga horária total do curso. 6. São obrigações do ALUNO: 6.1. Pagar a taxa de inscrição e apresentar todos seus documentos pessoais para efetivação da matrícula, bem como demais documentos que venham a ser solicitados a qualquer tempo pela CONTRATADA. Na hipótese de não apresentação dos documentos solicitados, a CONTRATADA poderá impedir o ALUNO de cursar as matérias em andamento até que seja realizada a regularização de seus documentos pessoais. 6.2. Pagar pontualmente as mensalidades devidas à CONTRATADA, no valor e na data de vencimento indicados no QUADRO DE CONTRATAÇÃO deste CONTRATO. 6.3. Cursar disciplinas e/ou atividades do curso escolhido no cronograma e na localidade em que estas forem regularmente ofertadas, de acordo com o projeto pedagógico e matriz curricular estabelecidos, ficando a CONTRATADA desobrigada de reabrir disciplinas e/ou atividades quando o curso não estiver sendo oferecido ou quando a turma não estiver tendo aulas. 6.4. Realizar sua rematrícula acadêmica ao término de cada trimestre. Nos termos da legislação vigente, a CONTRATADA não permitirá a rematrícula do ALUNO que (i) estiver inadimplente em relação ao pagamento de qualquer uma das parcelas previstas no QUADRO DE CONTRATAÇÃO, especialmente a inexistência de débito referente às mensalidades dos períodos anteriores e às taxas de biblioteca; (ii) não tiver enviado todos os documentos pessoais que lhe tenham sido requeridos pela CONTRATADA, a exemplo dos comprobatórios da sua conclusão do ensino médio e respectiva publicação em DO, quando for o caso; (iii) não tiver desempenho acadêmico compatível com a continuidade do curso; (iv) não cumprir com os Regulamentos da CONTRATADA. 6.5. Zelar pelas instalações e bens da CONTRATADA, respondendo por quaisquer danos comprovadamente causados. 6.6. Atualizar seu e-mail, endereço e telefone junto a secretaria do curso, pois a CONTRATADA não se responsabilizará por extravio de correspondências ou falta de comunicação sobre possíveis alterações no decorrer do curso, nem por boletos que sejam enviados para endereços errados ou insuficientes. 7. O ALUNO declara-se ciente de que: a) As parcelas mensais mencionadas no QUADRO DE CONTRATAÇÃO não tem nenhuma vinculação com o número de meses letivos em que perdurará o curso escolhido pelo aluno, motivo pela qual o ALUNO reconhece que as parcelas são devidas inclusive nos meses de férias ou recesso. b) O não comparecimento do ALUNO às aulas ou demais atos acadêmicos contratados não o exime ou eximirá do pagamento das parcelas relativas às referidas aulas ou atos acadêmicos realizados sem a sua presença, tendo em vista que o serviço de ensino terá sido efetivamente prestado e colocado à disposição do ALUNO. c) Não existe a possibilidade de trancamento ou suspensão dos serviços educacionais após o início das aulas. Caso o ALUNO desista, por qualquer motivo, de continuar frequentando o curso contratado, perderá todo o investimento e os pagamentos até então realizados, os quais não lhe serão devolvidos/ressarcidos em hipótese alguma ou sob qualquer fundamento, por constituírem receita para com o custeio do corpo docente e demais despesas para a constituição, manutenção e fornecimento do curso disponibilizado ao ALUNO. d) Em caso de atraso de pagamento de uma ou mais parcelas mensais, a CONTRATADA cobrará do ALUNO multa de 2% sobre a parcela devida, juros de mora de 1% ao mês ou fração de mês, e atualização monetária, bem como poderá, independentemente de prévia notificação, adotar todas as providências de cobrança cabíveis (inclusive inscrever o nome do ALUNO em cadastros ou serviços legalmente constituídos e destinados à proteção do crédito e cessar a prestação de serviços educacionais imediatamente), podendo tais providências serem tomadas isolada, gradativa ou cumulativamente. e) O presente CONTRATO vale como título executivo extrajudicial, nos termos do art. 585, II, do CPC, e o ALUNO reconhece e aceita que este CONTRATO é título executivo líquido, certo e exigível. f) Não poderá fumar em qualquer uma das dependências da CONTRATADA e, caso descumpra com tal vedação, a CONTRATADA poderá solicitar força policial para retirar o ALUNO de suas dependências, ficando este também obrigado por qualquer multa pecuniária que seja imposta à CONTRATADA em função de ato comprovadamente praticado pelo ALUNO. g) A CONTRATADA se resguarda no direito de captar e veicular a imagem do ALUNO, bem como os trabalhos acadêmicos por ele realizados, em meios de comunicação, folders ou outros materiais de comunicação relacionados exclusivamente à divulgação dos serviços acadêmicos prestados pela CONTRATADA, sem que caiba ao ALUNO qualquer direito ao recebimento de indenização ou remuneração. h) Qualquer requerimento formulado pelo ALUNO à CONTRATADA somente será válido se realizado em formulário próprio e protocolado no competente setor de atendimento da CONTRATADA em que o curso escolhido for ministrado e/ou por via on-line, bem como deverá ser firmado pelo próprio ALUNO. i) Não estão incluídos no valor deste CONTRATO: o fornecimento de materiais acadêmicos indicados e/ou solicitados pelos docentes para estudos curriculares; material didático, apostilas, cópias reprográficas e demais materiais utilizados em clínicas, laboratórios ou aulas práticas; serviços especiais de reforço de matérias, seminários, monografias; taxas e emolumentos; transporte escolar; guarda e responsabilidade sobre veículos, motocicletas e bicicletas deixados pelo(a) ALUNO nas dependências da CONTRATADA, ou imediações destas, bem como fornecimento de material pessoal e didático de uso individual do ALUNO. j) É assegurada à CONTRATADA encerrar as atividades educacionais na Unidade de origem do seu curso e/ou turno, sendo certo que serão facultados ao ALUNO a transferência para outra Unidade da CONTRATADA e o aproveitamento total dos valores já pagos e das disciplinas por ele já cursadas até a data do encerramento em referência. k) O SEI não se responsabiliza por dinheiro, objetos de valor, celulares e outros, como motocicletas ou bicicletas que não estejam sob guarda, perdidos ou desaparecidos em suas dependências. l) O pagamento efetuado mediante boleto, cheque ou ordem de pagamento bancário somente será considerado quitado após a compensação do mesmo, não sendo aceitos pagamentos mediante depósito bancário, sendo este procedimento considerado como inadimplemento. m) O ALUNO deverá comunicar a CONTRATADA mudança de endereço no prazo máximo de 30 (trinta) dias, sob pena de infração contratual. n) Na hipótese do pagamento da mensalidade em atraso, o ALUNO perderá o desconto concedido no mês, obrigando se ao pagamento integral da referida parcela, acrescida dos encargos legais e contratuais. o) Os descontos concedidos são de caráter transitório, não gerando direitos definitivos, devendo o ALUNO solicitar a renovação do benefício a cada módulo do curso. 8. São obrigações da CONTRATADA: 8.1. Oferecer o curso especificado no QUADRO DE CONTRATAÇÃO, em conformidade com o plano de estudos e a legislação, fixando sua carga horária, definindo e divulgando as datas das avaliações, bem como selecionando os professores qualificados para ministrar as disciplinas componentes do curso mencionado no QUADRO DE CONTRATAÇÃO. 9. O presente CONTRATO tem início na data da sua assinatura, subsistindo até o término do curso escolhido pelo ALUNO no QUADRO DE CONTRATAÇÃO. 10. Fica reservado à CONTRATADA o direito de não oferecer o curso escolhido pelo ALUNO no QUADRO DE CONTRATAÇÃO caso não se atinja um número mínimo de alunos efetivamente matriculados acadêmica e financeiramente. Nesta hipótese, o ALUNO deverá optar por aguardar o início de uma nova turma ou rescindir o CONTRATO, hipótese em que deverá apresentar formalmente o pedido de devolução da taxa de inscrição já paga, devolução esta que se efetivará no prazo de até 30 dias úteis após a data da apresentação do pedido de devolução à CONTRATADA. 11.Será facultado à CONTRATADA rescindir o presente CONTRATO pela prática comprovada de atos de indisciplina por parte do ALUNO, ou de atos previstos do Regimento Interno da CONTRATADA, sendo devidas as mensalidades até a data da efetiva expulsão do ALUNO. 11.1. Poderá também a CONTRATADA dar por rescindido o presente CONTRATO na hipótese de inadimplência do ALUNO, com fundamento no art. 5º da Lei 9.870, de 23 de novembro de 1999. 12. O presente Contrato poderá ser rescindido pelo ALUNO (cancelamento de matrícula) a qualquer tempo, cabendo-lhe, neste caso, o pagamento da(s) parcela(s) vencida(s) até a data da formalização do seu pedido, mais multa contratual de 10% das mensalidades restantes. A CONTRATADA não receberá o pedido de rescisão/cancelamento de matrícula de ALUNO inadimplente até que seja realizado o acerto da referida pendência financeira. 13.O ALUNO que for comprovadamente dispensado pela CONTRATADA de cursar determinadas disciplinas que compõe o curso por ele escolhido no QUADRO DE CONTRATAÇÃO não será dispensado de pagar pelas referidas disciplinas, descabendo-lhe o direito de pleitear da CONTRATADA qualquer reembolso, desconto ou indenização em decorrência de tal fato. 14. Ao assinar este contrato, o aluno toma ciência que as disciplinas de campo pratico, só será liberada caso o mesmo esteja com as mensalidades em dia e após a prova de habilitação para campo prático. 15. O uso do Uniforme no ambiente escolar é obrigatório. 16. A contratada se compromete em encaminhar os alunos para campo prático. Fica esclarecido que este ESTÁGIO não está condicionado a região de domicilio do CONTRATANTE e da respectiva CONTRATADA, podendo ocorrer em diversos outros locais do grande Rio de Janeiro e seus municípios vizinhos.  17. O aluno que faltar o período de prova poderá requerer segunda chamada e transferência interna ou externa, declaração e demais taxas não inclusas nas mensalidades mediante pagamento de taxa administrativa na secretaria da instituição; salvo em casos de problemas de saúde ou óbito de dependentes do contratante. 18. O aluno que optar pelo curso de Especialização/qualificação deverá apresentar no ato da matricula os seguintes documentos: Rg, CPF, comprovante de Residência, 2 fotos 3x4, título de eleitor, certidão de nascimento/ casado, diploma e histórico do ensino médio e Técnico e publicação do diário oficial, Coren. 19. O aluno que optar pelo curso de   enfermagem deverá apresentar no ato da matricula os seguintes documentos: Rg, Cpf, Comprovante De Residência, 2 Fotos 3x4, Título De Eleitor, Certidão De Nascimento e/ou Casamento, Diploma E Histórico Do Ensino Médio E Técnico E Publicação em Diário Oficial.  
20.Fica eleito o foro central da comarca da Cidade de São João de Meriti, para dirimir qualquer ação fundada no presente Contrato, renunciando as partes qualquer outro foro, por mais privilegiado que venha a ser.E, por estarem de acordo, assinam o presente contrato em 2(duas) vias de igual teor e forma.</ span ></ div > ";
            newContrato.pacoteId = typePacote1.Id;


            var totalContratos = _db.Contratos.Count();
            var contrato = new Contrato(totalContratos, newContrato.pacoteId, newContrato.titulo, newContrato.ativo, newContrato.observacao);
            contrato.AddConteudos(newContrato.conteudo);

            await _db.Contratos.AddAsync(contrato);
            await _db.SaveChangesAsync();


            await _db.Colaboradores.AddRangeAsync(
                new Colaborador(
                    0, "Desenvolvedor", "invictus@master.com", "12345678912",
                    "21555555555", "Desenvolvedor", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João da Silva", "joao@gmail.com", "58795248975",
                    "21548957898", "Professor", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João Teixeira", "joaoteix@gmail.com", "42961742072",
                    "21548957898", "Professor", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Joaquim José", "joaquim@gmail.com", "04605705015",
                    "21548957898", "Professor", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Andre Marques", "andre@gmail.com", "20511464037",
                    "21548957898", "Professor", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Silvio Santos", "silvio@gmail.com", "75448137032",
                    "21548957898", "Professor", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "Fausto Silva", "fausto@gmail.com", "53272733000",
                    "21548957898", "Professor", 0, salaUmUnidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
                new Colaborador(
                    0, "João Almeida", "joao2@gmail.com", "50409942065",
                    "21548957898", "Administrador", 0, unidadeCG.Id, null,
                    false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                    "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Mévio Tício", "mevio@gmail.com", "26902480001",
                "21548957898", "Professor", 0, salaUmUnidadeCG.Id, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Antonio Carlos", "antonio@gmail.com", "37490462045",
                "21548957898", "Administrador", 0, unidadeCG.Id, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Mario Silva", "mario@gmai.com", "52368455051",
                "21548957898", "Administrador", 0, salaUmUnidadeCG.Id, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ"),
            new Colaborador(
                0, "Luciano Huck", "luciano@gmai.com", "14269876093",
                "21548957898", "Administrador", 0, unidadeCG.Id, null,
                false, true, "23050102", "Avenida Cesário de Melo", "casa tal",
                "Campo Grande", "Rio de Janeiro", "RJ")
            );
            await _db.SaveChangesAsync();


            _admApp.ReadAndSaveExcelAlunosCGI();

            _admApp.ReadAndSaveExcelAlunosSJM();

            var planPag = new PlanoPagamento(typePacote1.Id, "Pacote Educa Mais Brasil", 4290, 0, "Mensal", true, 45, Convert.ToInt32(contrato.Id), true);

            return Ok();


        }

    }
}
