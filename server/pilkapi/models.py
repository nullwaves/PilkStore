from django.db import models


class Location(models.Model):
    parent = models.ForeignKey(
        'self', on_delete=models.CASCADE, null=True, blank=True)
    name = models.CharField(unique=True, max_length=60)
    description = models.TextField()
    image = models.ImageField(
        upload_to='pilk/location', default='pilk-default.jpg')


class Pilk(models.Model):
    location = models.ForeignKey(Location, on_delete=models.PROTECT)
    name = models.CharField(max_length=200)
    description = models.TextField()
    image = models.ImageField(
        upload_to='pilk/items', default='pilk-default.jpg')
