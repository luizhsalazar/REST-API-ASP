// Write your Javascript code.

$.ajax({
    url: "http://localhost:51848/api/cursos",
    success: function (response) {
        console.log(response);

        $.each(response, function (indexCurso, curso) {
            var html = '<div id=curso' + indexCurso + '>';
            html += '<h1>' + curso.nome + '</h1>';

            if (curso.cursoDisciplinas.length > 0) {
                $.each(curso.cursoDisciplinas, function (indexDisciplina, cursoDisciplina) {
                    html += '<h4>' + ++indexDisciplina + '. ' + cursoDisciplina.disciplina.nome + '</h4>';
                });
            } else {
                html += '<h4>' + 'Nenhuma disciplina vinculada.' + '</h4>';
            }

            if (curso.cursoAlunos.length > 0) {
                $.each(curso.cursoAlunos, function (indexAluno, cursoAluno) {
                    html += 'Alunos matriculados: <br><h5>' + ++indexAluno + '. ' + cursoAluno.aluno.nome + '</h5>';
                });
            } else {
                html += '<h5>' + 'Nenhum aluno matriculado.' + '</h5>';
            }

            $('.cursosList').append(html);
        });


    }
});
