from django.db import models
from django.conf import settings
from django.urls import reverse

class Car(models.Model):
    user = models.ForeignKey(settings.AUTH_USER_MODEL, blank=True, on_delete=models.CASCADE)
    brand = models.CharField('Марка', max_length=50)
    model = models.CharField('Модель', max_length=50)
    year = models.PositiveIntegerField('Год выпуска')
    price = models.PositiveBigIntegerField('Цена', help_text='руб')
    image = models.ImageField('Изображение', max_length=100)
    description = models.TextField('Описание', blank=True)
    published_at = models.DateTimeField('Дата публикации', blank=True, auto_now_add=True)

    def __str__(self):
        return f'{self.brand} {self.model} {self.year}'

    def get_absolute_url(self):
        return reverse('cars:detail', args=[self.id])   

    class Meta:
        verbose_name = 'Автомобиль'
        verbose_name_plural = 'Автомобили'
        ordering = ['-published_at']