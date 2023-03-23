from django.shortcuts import get_object_or_404, render, redirect
import requests
from cars.filters import CarFilter
from django.views.generic import DeleteView, UpdateView, DeleteView
from cars.forms import CarForm
from django.views.generic.edit import CreateView
from cars.models import Car
from django.urls import reverse_lazy
from django.contrib.auth.mixins import LoginRequiredMixin

from cars.models import Car
from cars.utils import get_car_data

def index(request):
    filter = CarFilter(request.GET, queryset=Car.objects.all())
    context = {'filter': filter}
    return render(request, 'cars/index.html', context)


def detail(request, id):
    car = get_object_or_404(Car, id=id)
    context = {'car': car}
    return render(request, 'cars/detail.html', context)


def about(request):
    return render(request, 'cars/about.html')


def rate(request):
    if request.method == 'GET':
        form = CarForm()
        return render(request, 'cars/rate.html', {'form': form})

    form = CarForm(request.POST)
    if not form.is_valid():
        return render(request, 'cars/rate.html', {'form': form})

    context = {'form': CarForm()}

    try:
        car_data = get_car_data(form.data['brand'], form.data['model'], form.data['min_year'], form.data['max_year'], 5)
    except requests.ConnectionError as err:
        context['error'] = 'No internet connection' # Not true. May be not about internet connection.
    else:
        if car_data is None:
            context['error'] = 'No data found for that car'
        else:
            context['average_price'] = car_data['average_price']
            context['rating'] = car_data['rating']

    return render(request, 'cars/rate.html', context)


class CarCreateView(LoginRequiredMixin, CreateView):
    model = Car
    fields = ['brand', 'model', 'year', 'price', 'image', 'description']
    template_name = 'cars/create.html'

    def form_valid(self, form):
        form.instance.user = self.request.user
        return super().form_valid(form)


class CarDeleteView(LoginRequiredMixin, DeleteView):
    model = Car
    success_url = reverse_lazy('cars:index')
    pk_url_kwarg = 'id'

    def delete(self, *args, **kwargs):
        car = self.get_object()
        user = self.request.user

        if car.user != user:
            return redirect('cars:index')

        return super().delete(*args, **kwargs)