import re
import requests
from bs4 import BeautifulSoup


def get_car_data(brand, model, min_year, max_year, num_pages=10):
    car_data = {}

    brand = brand.lower()
    model = model.lower()

    for i in range(1, num_pages + 1):
        url = f'https://auto.drom.ru/{brand}/{model}/page{i}/?minyear={min_year}&maxyear={max_year}'
        response = requests.get(url)
 
        html = response.text
        soup = BeautifulSoup(html, 'html.parser')

        rating = soup.find(attrs={'data-ftid': 'component_rating'})
        if not rating:
            return None
        rating_value = rating.contents[-1]
                
        prices = soup.find_all(attrs={'data-ftid': 'bull_price'})
        if not prices:
            return None
        prices_values = [int(re.sub(r'\s', '', price.get_text())) for price in prices]

        car_data['rating'] = float(rating_value)
        car_data['average_price'] = sum(prices_values) / len(prices_values)
    
    return car_data