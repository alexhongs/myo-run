##### Performing PCA ######
import numpy as np
import sys
import matplotlib.pyplot as plt
import string
from sklearn.preprocessing import StandardScaler

outfile_data = "preliminary_data_filtered.npy"
outfile_labels = "preliminary_data_labels.npy"

npData = np.load(outfile_data)
labels = np.load(outfile_labels)

n = npData[:,1,1].size # number of samples
t = npData[1,:,1].size # number of time stamps
e = npData[1,1,:].size # number of electrodes


# mu = np.zeros([t, e])
# for i in range(n):
#     mu += npData[i,:,:]
# mu = mu / n

# # 300 x 5 = 1500
# # 1500 x 1
# # [1500 x 42] * [42 x 1500]

# difference = np.zeros([t*e, n])

# for i in range(n):
#     difference[:,i] = np.reshape(npData[i,:,:] - mu, (t*e))

# covariance = np.cov(difference)

# eig_val_cov, eig_vec_cov = np.linalg.eig(covariance)

# v = eig_vec_cov[0]
# w0 = -1*(matmul(v,mu))
# reconstructed = (v*(v.T*(c-mu)) + mu

# MSE = sum(sum((c - reconstructed).^2 / n));

# print(eig_vec_cov[0].size)

# print(eig_vec_cov[:,1].shape)

# print(npData[0,:,:])