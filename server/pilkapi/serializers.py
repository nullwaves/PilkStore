from django.contrib.auth.models import User
from rest_framework import serializers

from pilkapi.models import Location, Pilk


class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ['url', 'username', 'email', 'groups']


class LocationSerializer(serializers.ModelSerializer):
    class Meta:
        model = Location
        fields = ['pk', 'url', 'parent', 'name', 'description', 'image',
                  'children', 'items']
        extra_kwargs = {'image': {'required': False}}


class PilkSerializer(serializers.ModelSerializer):
    class Meta:
        model = Pilk
        fields = ['pk', 'url', 'location', 'name', 'description', 'image']
        extra_kwargs = {'image': {'required': False}}
