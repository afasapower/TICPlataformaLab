var gulp = require('gulp'),
    gulpCleanCss = require('gulp-clean-css'),
    minifyjs = require('gulp-js-minify'),
    concat = require('gulp-concat'),
    clean = require('gulp-clean');

gulp.task('limpar', function () {
    return gulp.src('wwwroot/js/dist/*.js', { force: true }).pipe(clean());    
});

gulp.task('default', function ()
{
    gulp.run('minify-js', 'concat');    
});

gulp.task('minify-js', function()
{
    console.log('minify-js');
    gulp.src('./wwwroot/js/chancelaria/**/*.js').pipe(minifyjs()).pipe(gulp.dest('wwwroot/js/min/chancelaria/'));
    gulp.src('./wwwroot/js/sistema/**/*.js').pipe(minifyjs()).pipe(gulp.dest('wwwroot/js/min/sistema/'));
    gulp.src('./wwwroot/js/tribunais/**/*.js').pipe(minifyjs()).pipe(gulp.dest('wwwroot/js/min/tribunais/'));
    gulp.src(['./wwwroot/js/Listas.js',
        './wwwroot/js/menuAbaScroll.js',
        './wwwroot/js/Navegacao.js',
        './wwwroot/js/MenuEdicaoAbas.js',
        './wwwroot/js/Erros.js',
        './wwwroot/js/Calendario.js',
        './wwwroot/js/Graficos.js',
        './wwwroot/js/Contas.js',
        './wwwroot/js/Formularios.js',
        './wwwroot/js/EditorTexto.js',       
        './wwwroot/js/geral.js'])        
        .pipe(minifyjs())
        .pipe(gulp.dest('wwwroot/js/dist/'));
});

gulp.task('concat', function ()
{
    console.log('concat');
    gulp.src(['./wwwroot/js/dist/EditorTexto.js',
        './wwwroot/js/dist/Contas.js',
        './wwwroot/js/dist/Graficos.js',
        './wwwroot/js/dist/Calendario.js',
        './wwwroot/js/dist/Erros.js',
        './wwwroot/js/dist/Listas.js',
        './wwwroot/js/dist/menuAbaScroll.js',
        './wwwroot/js/dist/MenuEdicaoAbas.js',
        './wwwroot/js/dist/Formularios.js',
        './wwwroot/js/dist/Navegacao.js',
        './wwwroot/js/dist/geral.js'])     
        .pipe(concat('all.js'))
        .pipe(gulp.dest('wwwroot/js/min/'));
});

gulp.task('concateste',function ()
{
    console.log('concat teste');
    gulp.src(['./wwwroot/js/dist/EditorTexto.js']).pipe(concat('EditorTexto.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Contas.js']).pipe(concat('Contas.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Graficos.js']).pipe(concat('Graficos.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Calendario.js']).pipe(concat('Calendario.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Erros.js']).pipe(concat('Erros.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Listas.js']).pipe(concat('Listas.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/menuAbaScroll.js']).pipe(concat('menuAbaScroll.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/MenuEdicaoAbas.js']).pipe(concat('MenuEdicaoAbas.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Formularios.js']).pipe(concat('Formularios.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/Navegacao.js']).pipe(concat('Navegacao.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/geral.js']).pipe(concat('geral.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));
    gulp.src(['./wwwroot/js/dist/EditorTexto.js']).pipe(concat('EditorTexto.min.js')).pipe(gulp.dest('wwwroot/js/min/teste/'));        
});

gulp.task('testes', function () {
    gulp.run('limpar','minify-js', 'concateste');
});






   
// Gulp não aceita declaração de função assim na aplicação:
// var nomedaFuncao = function() { }
// Declarar sempre assim:
// function nomeDaFuncao() { }
   

