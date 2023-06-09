# Generated by Django 4.0 on 2022-01-07 08:15

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('auth', '0012_alter_user_first_name_max_length'),
        ('cars', '0002_car_user_alter_car_price'),
    ]

    operations = [
        migrations.AlterModelOptions(
            name='car',
            options={'ordering': ['-published_at'], 'verbose_name': 'Автомобиль', 'verbose_name_plural': 'Автомобили'},
        ),
        migrations.AlterField(
            model_name='car',
            name='description',
            field=models.TextField(blank=True, verbose_name='Описание'),
        ),
        migrations.AlterField(
            model_name='car',
            name='price',
            field=models.PositiveBigIntegerField(help_text='руб', verbose_name='Цена'),
        ),
        migrations.AlterField(
            model_name='car',
            name='user',
            field=models.ForeignKey(blank=True, on_delete=django.db.models.deletion.CASCADE, to='auth.user'),
        ),
    ]
